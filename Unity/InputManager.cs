using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Event;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public class InputManager
	{
		private readonly List<ABaseInputDevice> m_InputDevices = new List<ABaseInputDevice>();

		private int m_UpdateTick;
		protected readonly List<InputLayer> m_ActiveLayers = new List<InputLayer>();
		private readonly List<IInputReceiver> m_ToPop = new List<IInputReceiver>();
		private readonly List<InputLayer> m_ToPush = new List<InputLayer>();
		private readonly InputValidator m_Validator = new InputValidator();

		private EDeviceID m_ActiveDevice = EDeviceID.Mouse;
		public EDeviceID ActiveDevice => m_ActiveDevice;
		public IReadOnlyList<InputLayer> ActiveLayers => m_ActiveLayers;

		public readonly CustomEvent<EDeviceID> OnActiveDeviceChanged = new CustomEvent<EDeviceID>();

		public InputManager()
		{
			RegisterDevices();
		}

		public virtual void Update(float deltaTime)
		{
			++m_UpdateTick;

			bool hasFocus = Application.isFocused;
			if(hasFocus)
			{
				UpdateDevices();
			}
			else
			{
				ResetDevices();
			}
			UpdateActiveLayers(deltaTime);
		}

		#region Devices
		protected virtual void RegisterDevices()
		{
			RegisterDevice(TouchDevice.Instance);
			RegisterDevice(KeyboardDevice.Instance);
			RegisterDevice(MouseDevice.Instance);

			int length = GamepadDevice.Gamepads.Length;
			for(int x = 0; x < length; ++x)
			{
				RegisterDevice(GamepadDevice.Gamepads[x]);
			}
			RegisterDevice(AnyGamepadDevice.Instance);
		}

		private void UpdateDevices()
		{
			EDeviceID newDevice = m_ActiveDevice;
			bool deviceChanged = false;

			int count = m_InputDevices.Count;
			for(int x = 0; x < count; ++x)
			{
				ABaseInputDevice device = m_InputDevices[x];
				if(device.Enabled)
				{
					device.UpdateState(m_UpdateTick);
					if(!deviceChanged && device.IsActive)
					{
						newDevice = device.ID;
						deviceChanged = true;
					}
				}
			}

			TrySetActiveDevice(newDevice);
		}

		private void ResetDevices()
		{
			int count = m_InputDevices.Count;
			for(int x = 0; x < count; ++x)
			{
				ABaseInputDevice device = m_InputDevices[x];
				device.ResetState();
			}
		}
		#endregion

		#region Device Management
		public bool RegisterDevice(ABaseInputDevice device, bool priority = false)
		{
			if(!m_InputDevices.Contains(device))
			{
				if(priority)
				{
					m_InputDevices.Insert(0, device);
				}
				else
				{
					m_InputDevices.Add(device);
				}
				return true;
			}
			return false;
		}

		public bool UnregisterDevice(ABaseInputDevice device)
		{
			return m_InputDevices.Remove(device);
		}

		public void TrySetActiveDevice(EDeviceID deviceID)
		{
			if(m_ActiveDevice != deviceID)
			{
				Cursor.visible = (deviceID == EDeviceID.Mouse || deviceID == EDeviceID.Keyboard);
				m_ActiveDevice = deviceID;
				OnActiveDeviceChanged.Invoke(m_ActiveDevice);
			}
		}

		public void SetActiveDevice(ABaseInputDevice device)
		{
			TrySetActiveDevice(device.ID);
		}
		#endregion

		#region Layers
		private void UpdateActiveLayers(float deltaTime)
		{
			for(int x = m_ToPop.Count - 1; x >= 0; --x)
			{
				IInputReceiver receiver = m_ToPop[x];
				int index = IndexOfReceiver(receiver);
				InputLayer layer = m_ActiveLayers[index];
				m_Validator.Remove(layer.Updater);
				m_ActiveLayers.RemoveAt(index);
			}
			m_ToPop.Clear();

			bool canProceed = true;
			int count = m_ActiveLayers.Count;
			int lastValid = -1;
			for(int x = 0; x < count; ++x)
			{
				InputLayer layer = m_ActiveLayers[x];
				layer.IsActive = canProceed;
				if(canProceed)
				{
					lastValid = x;
					layer.Updater.Update(m_UpdateTick, deltaTime);
				}
				if(layer.Definition.Block)
				{
					canProceed = false;
				}
			}
			m_Validator.Update();

			for(int x = 0; x <= lastValid; ++x)
			{
				InputLayer layer = m_ActiveLayers[x];
				layer.Updater.Validate(m_Validator);
				layer.Receiver.UpdateInput(this);
			}

			int toPushCount = m_ToPush.Count;
			for(int x = 0; x < toPushCount; ++x)
			{
				InputLayer layer = m_ToPush[x];
				PushReceiverInternal(layer);
			}
			m_ToPush.Clear();
		}
		#endregion

		#region Receiver
		public void PushReceiver(IInputReceiver receiver, DetectorUpdater updater, InputLayerDefinition layerDefinition)
		{
			InputLayer newLayer = new InputLayer(receiver, updater, layerDefinition);
			m_ToPush.Add(newLayer);
		}

		private void PushReceiverInternal(InputLayer newLayer)
		{
			if(IndexOfReceiver(newLayer.Receiver) >= 0)
			{
				if(!m_ToPop.Remove(newLayer.Receiver))
				{
					Debug.LogException(new ArgumentException(string.Format("Receiver {0} is already active", newLayer.Receiver.ToString())));
				}
				return;
			}

			m_Validator.Add(newLayer.Updater);

			int count = m_ActiveLayers.Count;
			for(int x = 0; x < count; ++x)
			{
				InputLayer layer = m_ActiveLayers[x];
				if(layer.Definition.Priority < newLayer.Definition.Priority)
				{
					m_ActiveLayers.Insert(x, newLayer);
					return;
				}
			}
			m_ActiveLayers.Add(newLayer);
		}

		public bool PopReceiver(IInputReceiver receiver)
		{
			int index = IndexOfReceiver(receiver);
			if(index == -1)
			{
				int length = m_ToPush.Count;
				for(int x = 0; x < length; ++x)
				{
					InputLayer toPushLayer = m_ToPush[x];
					if(toPushLayer.Receiver == receiver)
					{
						m_ToPush.RemoveAt(x);
						return true;
					}
				}
				return false;
			}
			m_ToPop.Add(receiver);
			return true;
		}

		protected int IndexOfReceiver(IInputReceiver receiver)
		{
			int count = m_ActiveLayers.Count;

			for(int x = 0; x != count; ++x)
			{
				InputLayer layer = m_ActiveLayers[x];
				if(layer.Receiver == receiver)
				{
					return x;
				}
			}
			return -1;
		}
		#endregion

		#region Providers
		public void GetActiveProviders(List<AInputProvider> providers)
		{
			providers.Clear();
			int count = m_InputDevices.Count;
			for(int x = 0; x < count; ++x)
			{
				ABaseInputDevice device = m_InputDevices[x];
				device.GetActiveProviders(providers);
			}
		}
		#endregion
	}
}

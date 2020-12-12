using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Event;
using ProceduralLevel.Common.Ext;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class InputManager: MonoBehaviour
	{
		public readonly KeyboardDevice Keyboard = new KeyboardDevice();
		public readonly MouseDevice Mouse = new MouseDevice();
		public readonly TouchDevice Touch = new TouchDevice();
		public readonly GamepadDevice[] Gamepads = new GamepadDevice[] {
			new GamepadDevice(EGamepadID.P1),
			new GamepadDevice(EGamepadID.P2),
			new GamepadDevice(EGamepadID.P3),
			new GamepadDevice(EGamepadID.P4)
		};
		public readonly AnyGamepadDevice AnyGamepad = new AnyGamepadDevice();

		private readonly List<AInputDevice> m_InputDevices = new List<AInputDevice>();

		private int m_UpdateTick;
		protected readonly List<InputLayer> m_ActiveLayers = new List<InputLayer>();
		private readonly List<IInputReceiver> m_ToPop = new List<IInputReceiver>();
		private readonly List<InputLayer> m_ToPush = new List<InputLayer>();
		public List<LayerDefinition> LayerDefinitions = new List<LayerDefinition>();

		public virtual Type EnumIDType { get { return null; } }
		public float DeltaTime { get; private set; }
		public int UpdateTick { get { return m_UpdateTick; } }

		private EDeviceID m_ActiveDevice = EDeviceID.Mouse;
		public EDeviceID ActiveDevice { get { return m_ActiveDevice; } }

		public readonly CustomEvent<EDeviceID> OnActiveDeviceChanged = new CustomEvent<EDeviceID>();

		private void Awake()
		{
			RegisterDevice(Touch);
			RegisterDevice(Keyboard);
			RegisterDevice(Mouse);

			int length = Gamepads.Length;
			for(int x = 0; x < length; ++x)
			{
				RegisterDevice(Gamepads[x]);
			}
			RegisterDevice(AnyGamepad);
		}

		public bool RegisterDevice(AInputDevice device, bool priority = false)
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

		public bool UnregisterDevice(AInputDevice device)
		{
			return m_InputDevices.Remove(device);
		}

		private void OnDestroy()
		{
			OnActiveDeviceChanged.RemoveAllListeners();
		}

		private void Update()
		{
			++m_UpdateTick;
			DeltaTime = Time.deltaTime;

			UpdateDevices();
			UpdateActiveLayers();
		}

		private void UpdateDevices()
		{
			EDeviceID oldDevice = m_ActiveDevice;
			bool deviceChanged = false;

			int count = m_InputDevices.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputDevice device = m_InputDevices[x];
				if(device.Enabled)
				{
					device.UpdateState(this);
					if(!deviceChanged && device.IsActive)
					{
						m_ActiveDevice = device.ID;
						deviceChanged = true;
					}
				}
			}
			if(oldDevice != m_ActiveDevice)
			{
				OnActiveDeviceChanged.Invoke(m_ActiveDevice);
			}
		}

		public void SetActiveDevice(EDeviceID id)
		{
			if(m_ActiveDevice != id)
			{
				m_ActiveDevice = id;
				OnActiveDeviceChanged.Invoke(m_ActiveDevice);
			}
		}

		public void SetActiveDevice(AInputDevice device)
		{
			SetActiveDevice(device.ID);
		}

		public void RecordProviders(List<AInputProvider> links)
		{
			links.Clear();
			int count = m_InputDevices.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputDevice device = m_InputDevices[x];
				device.RecordProviders(links);
			}
		}

		#region Layers
		private void UpdateActiveLayers()
		{
			for(int x = m_ToPop.Count-1; x >= 0; --x)
			{
				IInputReceiver receiver = m_ToPop[x];
				int index = IndexOfReceiver(receiver);
				m_ActiveLayers.RemoveAt(index);
			}
			m_ToPop.Clear();

			bool canProceed = true;
			int count = m_ActiveLayers.Count - 1;
			for(int x = count; x >= 0; --x)
			{
				InputLayer layer = m_ActiveLayers[x];
				layer.IsActive = canProceed;
				if(canProceed)
				{
					layer.Receiver.UpdateInput(this);
				}
				if(layer.Definition.Block)
				{
					canProceed = false;
				}
			}

			int toPushCount = m_ToPush.Count;
			for(int x = 0; x < toPushCount; ++x)
			{
				InputLayer layer = m_ToPush[x];
				PushReceiverInternal(layer);
			}
			m_ToPush.Clear();
		}

		public List<InputLayer> GetActiveLayers()
		{
			return m_ActiveLayers;
		}

		protected LayerDefinition GetLayerDefinition(int layerID)
		{
			int count = LayerDefinitions.Count;
			for(int x = 0; x != count; ++x)
			{
				LayerDefinition definition = LayerDefinitions[x];
				if(definition.ID == layerID)
				{
					return definition;
				}
			}
			return null;
		}
		#endregion

		#region Receiver
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

		public void PushReceiver(IInputReceiver receiver, int layerID)
		{
			LayerDefinition definition = GetLayerDefinition(layerID);
			if(definition == null)
			{
				Debug.LogException(new ArgumentNullException(string.Format("Layer with ID: {0} was not found.", layerID)));
			}

			InputLayer newLayer = new InputLayer(receiver, definition);
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

			int count = m_ActiveLayers.Count;
			for(int x = 0; x != count; ++x)
			{
				InputLayer layer = m_ActiveLayers[x];
				if(layer.Definition.Priority > newLayer.Definition.Priority)
				{
					m_ActiveLayers.Insert(x, newLayer);
					return;
				}
			}
			m_ActiveLayers.Add(newLayer);
		}
		#endregion

		#region Gamepad
		public AGamepadDevice GetGamepad(EGamepadID id)
		{
			if(id == EGamepadID.Any)
			{
				return AnyGamepad;
			}
			int length = Gamepads.Length;
			for(int x = 0; x < length; ++x)
			{
				AGamepadDevice gamepad = Gamepads[x];
				if(gamepad.GamepadID == id)
				{
					return gamepad;
				}
			}
			return null;
		}

		public Gamepad GetUnityGamepad(EGamepadID id)
		{
			ReadOnlyArray<Gamepad> gamepads = Gamepad.all;
			int count = gamepads.Count;
			int intID = (int)id-1;
			if(intID < 0 || count <= intID)
			{
				return null;
			}
			return gamepads[intID];
		}
		#endregion

		protected void DrawDebugGUI()
		{
			TouchData[] touches = Touch.Touches;
			int touchCount = Touch.Count;
			for(int x = 0; x < touchCount; ++x)
			{
				TouchData touch = touches[x];
				GUILayout.Label(touch.ToString());
			}

			List<AInputProvider> providers = new List<AInputProvider>();
			RecordProviders(providers);
			if(providers.Count > 0)
			{
				string str = StringExt.JoinToString(providers);
				GUILayout.Label(str);
			}
		}
	}
}

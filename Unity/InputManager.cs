using System;
using System.Collections.Generic;
using UnityPlugins.Common.Logic;
using UnityEngine;

namespace UnityPlugins.Input.Unity
{
	public class InputManager
	{
		public static readonly InputManager Instance = new InputManager();

		private readonly List<AInputDevice> m_InputDevices = new List<AInputDevice>();

		private int m_UpdateTick;

		public readonly InputReceiversManager Receivers = new InputReceiversManager();

		private AInputDevice m_ActiveDevice;
		public AInputDevice ActiveDevice => m_ActiveDevice;

		public readonly CustomEvent<AInputDevice> OnActiveDeviceChanged = new CustomEvent<AInputDevice>();

		private InputManager()
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
			Receivers.Update(deltaTime);
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
			AInputDevice newDevice = m_ActiveDevice;
			bool deviceChanged = false;

			int count = m_InputDevices.Count;

			for(int x = 0; x < count; ++x)
			{
				AInputDevice device = m_InputDevices[x];
				if(device.Enabled)
				{
					try
					{
						device.Update(m_UpdateTick);
					}
					catch(Exception e)
					{
						Debug.LogException(e);
					}
					if(!deviceChanged && device.IsActive)
					{
						newDevice = device;
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
				AInputDevice device = m_InputDevices[x];
				device.ResetState();
			}
		}
		#endregion

		#region Device Management
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

		public void TrySetActiveDevice(AInputDevice device)
		{
			if(m_ActiveDevice != device)
			{
				Cursor.visible = device.ShowCursor;
				m_ActiveDevice = device;
				OnActiveDeviceChanged.Invoke(m_ActiveDevice);
			}
		}

		public void SetActiveDevice(AInputDevice device)
		{
			TrySetActiveDevice(device);
		}
		#endregion

		#region Providers
		public void GetActiveProviders(List<AInputProvider> providers)
		{
			providers.Clear();
			int count = m_InputDevices.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputDevice device = m_InputDevices[x];
				device.GetActiveProviders(providers);
			}
		}
		#endregion
	}
}

using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace UnityPlugins.Input.Unity
{
	public class AnyGamepadDevice : AGamepadDevice
	{
		public static readonly AnyGamepadDevice Instance = new AnyGamepadDevice();

		private GamepadDevice m_ActiveGamepad;

		public override Gamepad UnityGamepad => m_ActiveGamepad?.UnityGamepad;
		public override EGamepadType GamepadType => (m_ActiveGamepad != null ? m_ActiveGamepad.GamepadType : EGamepadType.Generic);
		protected override int CompareValue => -1;

		public AnyGamepadDevice()
		{

		}

		#region Getters
		public override RawInputState Get(EGamepadInputID inputID)
		{
			if(m_ActiveGamepad != null)
			{
				return m_ActiveGamepad.Get(inputID);
			}
			return new RawInputState();
		}

		public override float GetAxis(EGamepadInputID inputID)
		{
			if(m_ActiveGamepad != null)
			{
				return m_ActiveGamepad.GetAxis(inputID);
			}
			return 0f;
		}
		#endregion

		#region Update State
		protected override void OnUpdate()
		{
			base.OnUpdate();

			GamepadDevice[] gamepads = GamepadDevice.Gamepads;
			int length = gamepads.Length;
			for(int x = 0; x < length; ++x)
			{
				GamepadDevice device = gamepads[x];
				if(device.IsActive)
				{
					m_ActiveGamepad = device;
					break;
				}
			}
			if(m_ActiveGamepad != null)
			{
				m_IsActive = m_ActiveGamepad.IsActive;
				m_IsAnyKeyActive = m_ActiveGamepad.IsAnyKeyActive;
			}
		}

		protected override RawInputState GetState(int rawInputID)
		{
			if(m_ActiveGamepad != null)
			{
				return new RawInputState(m_ActiveGamepad.Get((EGamepadInputID)rawInputID).IsActive);
			}
			return new RawInputState(false);
		}
		#endregion

		public override void Rumble(float low, float high)
		{
			if(m_ActiveGamepad != null)
			{
				m_ActiveGamepad.Rumble(low, high);
			}
		}

		public override void GetActiveProviders(List<AInputProvider> providers)
		{
			if(IsActive)
			{
				int length = m_InputState.Length;
				for(int x = 0; x < length; ++x)
				{
					RawInputState state = m_InputState[x];
					if(state.IsActive)
					{
						providers.Add(new GamepadProvider((EGamepadInputID)x, this));
					}
				}
			}
		}
	}
}

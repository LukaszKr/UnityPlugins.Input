using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class AnyGamepadDevice: AGamepadDevice
	{
		private GamepadDevice m_ActiveGamepad;

		public override Gamepad UnityGamepad { get { return m_ActiveGamepad?.UnityGamepad; } }
		public override EGamepadType GamepadType { get { return (m_ActiveGamepad != null? m_ActiveGamepad.GamepadType: EGamepadType.Generic); } }

		public AnyGamepadDevice()
			: base(EGamepadID.Any)
		{

		}

		public override InputState Get(EGamepadButton button)
		{
			if(m_ActiveGamepad != null)
			{
				return m_ActiveGamepad.Get(button);
			}
			return new InputState();
		}

		public override EInputStatus GetStatus(EGamepadButton button)
		{
			if(m_ActiveGamepad != null)
			{
				return m_ActiveGamepad.GetStatus(button);
			}
			return EInputStatus.Released;
		}

		public override float GetAxis(EGamepadButton button)
		{
			if(m_ActiveGamepad != null)
			{
				return m_ActiveGamepad.GetAxis(button);
			}
			return 0f;
		}

		public override void Rumble(float low, float high)
		{
			if(m_ActiveGamepad != null)
			{
				m_ActiveGamepad.Rumble(low, high);
			}
		}

		protected override RawInputState GetRawState(int inputID)
		{
			if(m_ActiveGamepad != null)
			{
				InputState state = m_ActiveGamepad.Get((EGamepadButton)inputID);
				return new RawInputState(state);
			}
			return new RawInputState(false);
		}

		protected override void OnUpdateState(InputManager inputManager)
		{
			GamepadDevice[] gamepads = inputManager.Gamepads;
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
			}
		}
	}
}

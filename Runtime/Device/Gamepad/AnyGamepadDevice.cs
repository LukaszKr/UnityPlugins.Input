using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class AnyGamepadDevice: AGamepadDevice
	{
		private GamepadDevice m_ActiveGamepad;

		public override Gamepad UnityGamepad { get { return m_ActiveGamepad?.UnityGamepad; } }
		public override EGamepadType GamepadType { get { return (m_ActiveGamepad != null? m_ActiveGamepad.GamepadType: EGamepadType.Generic); } }

		public override EButtonState Get(EGamepadButton button)
		{
			if(m_ActiveGamepad != null)
			{
				return m_ActiveGamepad.Get(button);
			}
			return EButtonState.Released;
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

		protected override bool IsPressed(int codeValue)
		{
			if(m_ActiveGamepad != null)
			{
				EButtonState buttonState = m_ActiveGamepad.Get((EGamepadButton)codeValue);
				return EButtonState.IsDown.Contains(buttonState);
			}
			return false;
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

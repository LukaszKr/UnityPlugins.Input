using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Haptics;

namespace ProceduralLevel.UnityPlugins.Input
{
	public partial class GamepadDevice: AGamepadDevice
	{
		public static float AxisDeadZone = 0.19f;
		public static float ButtonDeadZone = 0.5f;

		private readonly float[] m_AxesStates;

		private Gamepad m_Gamepad;
		private EGamepadType m_GamepadType = EGamepadType.Generic;

		private bool m_AnyAxisActive;

		public bool AnyAxisActive { get { return m_AnyAxisActive; } }

		public override Gamepad UnityGamepad { get { return m_Gamepad; } }
		public override EGamepadType GamepadType { get { return m_GamepadType; } }

		public GamepadDevice(EGamepadID gamepadID)
			: base(gamepadID)
		{
			m_AxesStates = new float[EGamepadButtonExt.MAX_VALUE+1];
		}

		public override EButtonState Get(EGamepadButton code)
		{
			return m_KeyStates[(int)code];
		}

		public override float GetAxis(EGamepadButton button)
		{
			if(button.IsAxis())
			{
				return m_AxesStates[(int)button];
			}
			else
			{
				return (EButtonState.IsDown.Contains(Get(button)) ? 1f : 0f);
			}
		}

		public override void Rumble(float low, float high)
		{
			IDualMotorRumble rumble = m_Gamepad as IDualMotorRumble;
			if(rumble != null)
			{
				rumble.SetMotorSpeeds(low, high);
			}
		}

		protected override void OnUpdateState(InputManager inputManager)
		{
			m_AnyAxisActive = false;
			m_Gamepad = inputManager.GetUnityGamepad(GamepadID);

			int axesCount = m_AxesStates.Length;
			for(int x = 0; x < axesCount; ++x)
			{
				m_AxesStates[x] = 0f;
			}

			if(m_Gamepad != null)
			{
				m_GamepadType = EGamepadTypeExt.FromGamepad(m_Gamepad);
				ProcessStick(m_Gamepad.leftStick, EGamepadButton.LStickUp, EGamepadButton.LStickDown, EGamepadButton.LStickLeft, EGamepadButton.LStickRight);
				ProcessStick(m_Gamepad.rightStick, EGamepadButton.RStickUp, EGamepadButton.RStickDown, EGamepadButton.RStickLeft, EGamepadButton.RStickRight);
				ProcessAxis(EGamepadButton.LTrigger, m_Gamepad.leftTrigger.ReadValue());
				ProcessAxis(EGamepadButton.RTrigger, m_Gamepad.rightTrigger.ReadValue());
			}

			m_IsActive = m_IsActive || m_AnyAxisActive;
		}

		private void ProcessAxis(EGamepadButton axis, float value)
		{
			m_AxesStates[(int)axis] = value;
			if(value >= AxisDeadZone)
			{
				m_AnyAxisActive = true;
			}
		}

		private void ProcessStick(StickControl stick, EGamepadButton up, EGamepadButton down, EGamepadButton left, EGamepadButton right)
		{
			Vector2 axes = stick.ReadValue();
			float valueX = axes.x;
			float valueY = axes.y;
			if(valueX > 0)
			{
				ProcessAxis(right, valueX);
			}
			else
			{
				ProcessAxis(left, -valueX);
			}
			if(valueY > 0)
			{
				ProcessAxis(up, valueY);
			}
			else
			{
				ProcessAxis(down, -valueY);
			}
		}

		protected override bool IsPressed(int codeValue)
		{
			if(m_Gamepad == null)
			{
				return false;
			}
			EGamepadButton button = (EGamepadButton)codeValue;
			if(codeValue <= (int)EGamepadButton.DPadDown)
			{
				return m_Gamepad[button.ToUnity()].isPressed;
			}
			else
			{
				return GetAxis(button) >= ButtonDeadZone;
			}
		}
	}
}

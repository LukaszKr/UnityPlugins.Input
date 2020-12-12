using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;

namespace ProceduralLevel.UnityPlugins.Input
{
	public partial class GamepadDevice: AGamepadDevice
	{
		public static float AxisDeadZone = 0.19f;

		private Gamepad m_Gamepad;
		private EGamepadType m_GamepadType = EGamepadType.Generic;

		public override Gamepad UnityGamepad { get { return m_Gamepad; } }
		public override EGamepadType GamepadType { get { return m_GamepadType; } }

		public GamepadDevice(EGamepadID gamepadID)
			: base(gamepadID)
		{
		}

		#region Getters
		public override InputState Get(EGamepadButton button)
		{
			return m_InputState[(int)button];
		}

		public override EInputStatus GetStatus(EGamepadButton button)
		{
			return m_InputState[(int)button].Status;
		}

		public override float GetAxis(EGamepadButton button)
		{
			return m_InputState[(int)button].Axis;
		}
		#endregion

		#region Update State
		protected override void OnUpdateState(InputManager inputManager)
		{
			m_Gamepad = inputManager.GetUnityGamepad(GamepadID);

			if(m_Gamepad != null)
			{
				m_GamepadType = EGamepadTypeExt.FromGamepad(m_Gamepad);
			}
		}

		protected override RawInputState GetRawState(int inputID)
		{
			if(m_Gamepad == null)
			{
				return new RawInputState(false);
			}
			EGamepadButton button = (EGamepadButton)inputID;
			if(button.IsAxis())
			{
				float axisValue = ReadAxisValue(button);
				return new RawInputState(axisValue >= AxisDeadZone, axisValue);
			}
			else
			{
				bool buttonState = m_Gamepad[button.ToUnity()].isPressed;
				return new RawInputState(buttonState);
			}
		}

		private float ReadAxisValue(EGamepadButton button)
		{
			switch(button)
			{
				case EGamepadButton.LStickRight:
					return Math.Max(0f, m_Gamepad.leftStick.ReadValue().x);
				case EGamepadButton.LStickLeft:
					return -Math.Min(0f, m_Gamepad.leftStick.ReadValue().x);
				case EGamepadButton.LStickUp:
					return Math.Max(0f, m_Gamepad.leftStick.ReadValue().y);
				case EGamepadButton.LStickDown:
					return -Math.Min(0f, m_Gamepad.leftStick.ReadValue().y);

				case EGamepadButton.RStickRight:
					return Math.Max(0f, m_Gamepad.rightStick.ReadValue().x);
				case EGamepadButton.RStickLeft:
					return -Math.Min(0f, m_Gamepad.rightStick.ReadValue().x);
				case EGamepadButton.RStickUp:
					return Math.Max(0f, m_Gamepad.rightStick.ReadValue().y);
				case EGamepadButton.RStickDown:
					return -Math.Min(0f, m_Gamepad.rightStick.ReadValue().y);

				case EGamepadButton.LTrigger:
					return m_Gamepad.leftTrigger.ReadValue();
				case EGamepadButton.RTrigger:
					return m_Gamepad.rightTrigger.ReadValue();
				default:
					throw new NotImplementedException();
			}
		}
		#endregion

		public override void Rumble(float low, float high)
		{
			IDualMotorRumble rumble = m_Gamepad as IDualMotorRumble;
			if(rumble != null)
			{
				rumble.SetMotorSpeeds(low, high);
			}
		}

		public override void RecordProviders(List<AInputProvider> providers)
		{
			//leave empty to avoid duplications from each of separate gamepads
		}
	}
}

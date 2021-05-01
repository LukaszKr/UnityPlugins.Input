using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;

namespace ProceduralLevel.UnityPlugins.Input
{
	public partial class GamepadDevice: AGamepadDevice
	{
		public static readonly GamepadDevice[] Gamepads = new GamepadDevice[]
		{
			new GamepadDevice(EGamepadID.P1),
			new GamepadDevice(EGamepadID.P2),
			new GamepadDevice(EGamepadID.P3),
			new GamepadDevice(EGamepadID.P4),
		};

		public float AxisDeadZone = 0.19f;

		private Gamepad m_Gamepad;
		private EGamepadType m_GamepadType = EGamepadType.Generic;

		public override Gamepad UnityGamepad { get { return m_Gamepad; } }
		public override EGamepadType GamepadType { get { return m_GamepadType; } }

		public GamepadDevice(EGamepadID gamepadID)
			: base(gamepadID)
		{
		}

		#region Getters
		public override InputState Get(EGamepadInputID inputID)
		{
			return m_InputState[(int)inputID];
		}

		public override EInputStatus GetStatus(EGamepadInputID inputID)
		{
			return m_InputState[(int)inputID].Status;
		}

		public override float GetAxis(EGamepadInputID inputID)
		{
			return m_InputState[(int)inputID].Axis;
		}
		#endregion

		#region Update State
		protected override void OnUpdateState(AInputManager inputManager)
		{
			m_Gamepad = inputManager.GetUnityGamepad(GamepadID);

			if(m_Gamepad != null)
			{
				m_GamepadType = EGamepadTypeExt.FromGamepad(m_Gamepad);
			}
		}

		protected override RawInputState GetRawState(int rawInputID)
		{
			if(m_Gamepad == null)
			{
				return new RawInputState(false);
			}
			EGamepadInputID inputID = (EGamepadInputID)rawInputID;
			if(inputID.IsAxis())
			{
				float axisValue = ReadAxisValue(inputID);
				return new RawInputState(axisValue >= AxisDeadZone, axisValue);
			}
			else
			{
				bool buttonState = m_Gamepad[inputID.ToUnity()].isPressed;
				return new RawInputState(buttonState);
			}
		}

		private float ReadAxisValue(EGamepadInputID button)
		{
			switch(button)
			{
				case EGamepadInputID.LStickRight:
					return Math.Max(0f, m_Gamepad.leftStick.ReadValue().x);
				case EGamepadInputID.LStickLeft:
					return -Math.Min(0f, m_Gamepad.leftStick.ReadValue().x);
				case EGamepadInputID.LStickUp:
					return Math.Max(0f, m_Gamepad.leftStick.ReadValue().y);
				case EGamepadInputID.LStickDown:
					return -Math.Min(0f, m_Gamepad.leftStick.ReadValue().y);

				case EGamepadInputID.RStickRight:
					return Math.Max(0f, m_Gamepad.rightStick.ReadValue().x);
				case EGamepadInputID.RStickLeft:
					return -Math.Min(0f, m_Gamepad.rightStick.ReadValue().x);
				case EGamepadInputID.RStickUp:
					return Math.Max(0f, m_Gamepad.rightStick.ReadValue().y);
				case EGamepadInputID.RStickDown:
					return -Math.Min(0f, m_Gamepad.rightStick.ReadValue().y);

				case EGamepadInputID.LTrigger:
					return m_Gamepad.leftTrigger.ReadValue();
				case EGamepadInputID.RTrigger:
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

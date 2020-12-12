﻿namespace ProceduralLevel.UnityPlugins.Input
{
	public class GamepadProvider: AButtonProvider
	{
		public EGamepadButton Button;
		public EGamepadID GamepadID;

		public GamepadProvider(EGamepadButton button, EGamepadID gamepadID = EGamepadID.Any)
		{
			Button = button;
			GamepadID = gamepadID;
		}

		public GamepadProvider(EGamepadButton button, AGamepadDevice gamepad = null)
		{
			Button = button;
			GamepadID = gamepad.GamepadID;
		}

		protected override EInputStatus GetButtonState(InputManager inputManager)
		{
			AGamepadDevice gamepad = inputManager.GetGamepad(GamepadID);
			return gamepad.GetStatus(Button);
		}

		public override string ToString()
		{
			return string.Format("[GamepadButton: {0}, GamepadID: {1}]", Button.ToString(), GamepadID.ToString());
		}
	}
}
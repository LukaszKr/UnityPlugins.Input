namespace ProceduralLevel.UnityPlugins.Input
{
	public class GamepadButtonProvider: AButtonProvider
	{
		public EGamepadButton Button;
		public AGamepadDevice Gamepad;

		public GamepadButtonProvider(EGamepadButton button, AGamepadDevice gamepad = null)
		{
			Button = button;
			Gamepad = gamepad;
		}

		protected override EButtonState GetButtonState(InputManager inputManager)
		{
			AGamepadDevice gamepad = (Gamepad == null? inputManager.AnyGamepad: Gamepad);
			return gamepad.Get(Button);
		}

		public override string ToString()
		{
			return string.Format("[GamepadButton: {0}, GamepadID: {1}]", Button.ToString(), Gamepad.ToString());
		}
	}
}

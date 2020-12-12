namespace ProceduralLevel.UnityPlugins.Input
{
	public class GamepadProvider: AInputProvider
	{
		public EGamepadButton Button;
		public EGamepadID GamepadID;

		public GamepadProvider(EGamepadButton button, EGamepadID gamepadID = EGamepadID.Any)
			: base(EDeviceID.Gamepad)
		{
			Button = button;
			GamepadID = gamepadID;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			AGamepadDevice gamepad = inputManager.GetGamepad(GamepadID);
			return gamepad.Get(Button).ToRaw();
		}

		protected override string ToStringImpl()
		{
			return $"{Button}, {GamepadID}";
		}
	}
}

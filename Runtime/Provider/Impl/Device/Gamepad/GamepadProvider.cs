namespace ProceduralLevel.UnityPlugins.Input
{
	public class GamepadProvider: ADeviceInputProvider
	{
		public EGamepadInputID InputID;
		public EGamepadID GamepadID;

		public GamepadProvider()
		{
		}

		public GamepadProvider(EGamepadInputID inputID, EGamepadID gamepadID = EGamepadID.Any)
		{
			InputID = inputID;
			GamepadID = gamepadID;
		}

		protected override RawInputState GetState(AInputManager inputManager)
		{
			AGamepadDevice gamepad = inputManager.GetGamepad(GamepadID);
			return gamepad.Get(InputID).ToRaw();
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			GamepadProvider otherProvider = (GamepadProvider)other;
			int gamepadCompare = GamepadID.CompareTo(otherProvider.GamepadID);
			if(gamepadCompare == 0)
			{
				return InputID.CompareTo(otherProvider.InputID);
			}
			return gamepadCompare;
		}

		protected override string ToStringImpl()
		{
			return $"{InputID}, {GamepadID}";
		}
	}
}

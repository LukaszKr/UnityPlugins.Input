namespace UnityPlugins.Input.Unity
{
	public class GamepadProvider : ADeviceInputProvider<GamepadProvider, EGamepadInputID>
	{
		public AGamepadDevice Gamepad;

		public GamepadProvider()
		{
		}

		public GamepadProvider(EGamepadInputID inputID, AGamepadDevice gamepad)
			: base(inputID)
		{
			m_InputID = inputID;
			Gamepad = gamepad;
		}

		public override RawInputState GetRawState()
		{
			return Gamepad.Get(m_InputID);
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			GamepadProvider otherProvider = (GamepadProvider)other;
			int gamepadCompare = Gamepad.CompareTo(otherProvider.Gamepad);
			if(gamepadCompare == 0)
			{
				return m_InputID.CompareTo(otherProvider.m_InputID);
			}
			return gamepadCompare;
		}

		protected override string ToStringImpl()
		{
			return $"{m_InputID}, {Gamepad}";
		}
	}
}

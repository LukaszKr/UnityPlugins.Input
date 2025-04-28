namespace UnityPlugins.Input.Unity
{
	public class GamepadProvider : ADeviceInputProvider<GamepadProvider, EGamepadInputID>
	{
		public EGamepadID GamepadID;

		public GamepadProvider()
		{
		}

		public GamepadProvider(EGamepadInputID inputID, EGamepadID gamepadID = EGamepadID.Any)
			: base(inputID)
		{
			m_InputID = inputID;
			GamepadID = gamepadID;
		}

		public override RawInputState GetRawState()
		{
			AGamepadDevice gamepad = GamepadID.GetGamepad();
			return gamepad.Get(m_InputID);
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			GamepadProvider otherProvider = (GamepadProvider)other;
			int gamepadCompare = GamepadID.CompareTo(otherProvider.GamepadID);
			if(gamepadCompare == 0)
			{
				return m_InputID.CompareTo(otherProvider.m_InputID);
			}
			return gamepadCompare;
		}

		protected override string ToStringImpl()
		{
			return $"{m_InputID}, {GamepadID}";
		}
	}
}

namespace ProceduralLevel.UnityPlugins.Input
{
	public class JoyConProvider: ADeviceInputProvider
	{
		public EJoyConInputID InputID;

		public JoyConProvider(EJoyConInputID button)
		{
			InputID = button;
		}

		protected override RawInputState GetState(InputManager inputManager)
		{
			return inputManager.JoyCon.Get(InputID).ToRaw();
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			JoyConProvider otherProvider = (JoyConProvider)other;
			return InputID.CompareTo(otherProvider.InputID);
		}

		protected override string ToStringImpl()
		{
			return $"{InputID}";
		}
	}
}

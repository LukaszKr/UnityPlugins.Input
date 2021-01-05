namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchProvider: AInputProvider
	{
		public ETouchID TouchID;

		public TouchProvider()
		{
		}

		public TouchProvider(ETouchID touchID)
		{
			TouchID = touchID;
		}

		protected override RawInputState GetState(InputManager inputManager)
		{
			return inputManager.Touch.Get(TouchID).ToRaw();
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			TouchProvider otherProvider = (TouchProvider)other;
			return TouchID.CompareTo(otherProvider.TouchID);
		}

		protected override string ToStringImpl()
		{
			return $"{TouchID}";
		}
	}
}

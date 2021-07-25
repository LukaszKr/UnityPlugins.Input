namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public class TouchProvider : ADeviceInputProvider
	{
		public ETouchInputID TouchID;

		public TouchProvider()
		{
		}

		public TouchProvider(ETouchInputID touchID)
		{
			TouchID = touchID;
		}

		protected override RawInputState GetState()
		{
			return TouchDevice.Instance.Get(TouchID).ToRaw();
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

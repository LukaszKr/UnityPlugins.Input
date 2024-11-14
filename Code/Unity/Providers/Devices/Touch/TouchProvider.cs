namespace UnityPlugins.Input.Unity
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

		public override RawInputState GetRawState()
		{
			return TouchDevice.Instance.Get(TouchID);
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

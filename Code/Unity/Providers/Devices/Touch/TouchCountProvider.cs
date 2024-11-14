namespace UnityPlugins.Input.Unity
{
	public class TouchCountProvider : ADeviceInputProvider
	{
		public int Count;

		public TouchCountProvider()
		{
		}

		public TouchCountProvider(int count)
		{
			Count = count;
		}

		public override RawInputState GetRawState()
		{
			return new RawInputState(TouchDevice.Instance.Count == Count);
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			TouchCountProvider otherProvider = (TouchCountProvider)other;
			return Count.CompareTo(otherProvider.Count);
		}

		protected override string ToStringImpl()
		{
			return $"{nameof(Count)}: {Count}";
		}
	}
}

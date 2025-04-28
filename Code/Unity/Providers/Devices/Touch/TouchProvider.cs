namespace UnityPlugins.Input.Unity
{
	public class TouchProvider : ADeviceInputProvider<TouchProvider, ETouchInputID>
	{
		public TouchProvider()
		{
		}

		public TouchProvider(ETouchInputID inputID)
			: base(inputID)
		{
		}

		public override RawInputState GetRawState()
		{
			return TouchDevice.Instance.Get(m_InputID);
		}
	}
}

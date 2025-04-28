namespace UnityPlugins.Input.Unity
{
	public class MouseProvider : ADeviceInputProvider<MouseProvider, EMouseInputID>
	{
		public MouseProvider()
		{
		}

		public MouseProvider(EMouseInputID inputID)
			: base(inputID)
		{
		}

		public override RawInputState GetRawState()
		{
			return MouseDevice.Instance.Get(m_InputID);
		}
	}
}

namespace UnityPlugins.Input.Unity
{
	public class MouseProvider : ADeviceInputProvider
	{
		public EMouseInputID InputID;

		public MouseProvider()
		{
		}

		public MouseProvider(EMouseInputID inputID)
		{
			InputID = inputID;
		}

		public override RawInputState GetRawState()
		{
			return MouseDevice.Instance.Get(InputID);
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			MouseProvider otherProvider = (MouseProvider)other;
			return InputID.CompareTo(otherProvider.InputID);
		}

		protected override string ToStringImpl()
		{
			return InputID.ToString();
		}
	}
}

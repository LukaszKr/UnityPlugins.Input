namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseInputLink: AInputLink
	{
		public readonly EMouseButton Button;

		public MouseInputLink(EMouseButton button) 
			: base(EDeviceID.Mouse)
		{
			Button = button;
		}

		protected override string ToStringImpl()
		{
			return $"{Button}";
		}
	}
}

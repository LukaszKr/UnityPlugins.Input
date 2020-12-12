namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchInputLink: AInputLink
	{
		public readonly ETouchID TouchID;

		public TouchInputLink(ETouchID touchID) 
			: base(EDeviceID.Touch)
		{
			TouchID = touchID;
		}

		protected override string ToStringImpl()
		{
			return $"{TouchID}";
		}
	}
}

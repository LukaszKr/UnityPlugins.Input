namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchProvider: AInputProvider
	{
		public ETouchID TouchID;

		public TouchProvider(ETouchID touchID)
			: base(EDeviceID.Touch)
		{
			TouchID = touchID;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			return inputManager.Touch.Get(TouchID).ToRaw();
		}

		protected override string ToStringImpl()
		{
			return $"{TouchID}";
		}
	}
}

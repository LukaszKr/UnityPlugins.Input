namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchProvider: AInputProvider
	{
		public ETouchID TouchID;

		public TouchProvider(ETouchID touchID)
		{
			TouchID = touchID;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			return inputManager.Touch.Get(TouchID).ToRaw();
		}
	}
}

namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchProvider: AButtonProvider
	{
		public ETouchID TouchID;

		public TouchProvider(ETouchID touchID)
		{
			TouchID = touchID;
		}

		protected override RawInputState GetInputStatus(InputManager inputManager)
		{
			return inputManager.Touch.Get(TouchID).ToRaw();
		}
	}
}

namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchProvider: AButtonProvider
	{
		public ETouchID TouchID;

		public TouchProvider(ETouchID touchID)
		{
			TouchID = touchID;
		}

		protected override EInputStatus GetButtonState(InputManager inputManager)
		{
			return inputManager.Touch.GetStatus(TouchID);
		}
	}
}

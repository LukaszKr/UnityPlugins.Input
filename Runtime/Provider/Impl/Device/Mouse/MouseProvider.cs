namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseProvider: AInputProvider
	{
		public EMouseInputID InputID;

		public MouseProvider()
		{
		}

		public MouseProvider(EMouseInputID inputID)
		{
			InputID = inputID;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			return inputManager.Mouse.Get(InputID).ToRaw();
		}

		protected override string ToStringImpl()
		{
			return $"{InputID}";
		}
	}
}

namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseProvider: AButtonProvider
	{
		public EMouseButton Button;

		public MouseProvider(EMouseButton button)
		{
			Button = button;
		}

		protected override RawInputState GetInputStatus(InputManager inputManager)
		{
			return inputManager.Mouse.Get(Button).ToRaw();
		}

		public override string ToString()
		{
			return string.Format("[MouseButton: {0}]", Button);
		}
	}
}

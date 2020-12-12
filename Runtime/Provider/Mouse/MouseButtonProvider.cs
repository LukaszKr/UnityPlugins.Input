namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseButtonProvider: AButtonProvider
	{
		public EMouseButton Button;

		public MouseButtonProvider(EMouseButton button)
		{
			Button = button;
		}

		protected override EInputStatus GetButtonState(InputManager inputManager)
		{
			return inputManager.Mouse.GetStatus(Button);
		}

		public override string ToString()
		{
			return string.Format("[MouseButton: {0}]", Button);
		}
	}
}

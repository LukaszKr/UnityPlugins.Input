namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseButtonProvider: AButtonProvider
	{
		public EMouseButton Button;

		public MouseButtonProvider(EMouseButton button)
		{
			Button = button;
		}

		protected override EButtonState GetButtonState(InputManager inputManager)
		{
			return inputManager.Mouse.Get(Button);
		}

		public override string ToString()
		{
			return string.Format("[MouseButton: {0}]", Button);
		}
	}
}

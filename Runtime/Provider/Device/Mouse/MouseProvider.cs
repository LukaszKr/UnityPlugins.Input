namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseProvider: AInputProvider
	{
		public EMouseButton Button;

		public MouseProvider(EMouseButton button)
			: base(EDeviceID.Mouse)
		{
			Button = button;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			return inputManager.Mouse.Get(Button).ToRaw();
		}

		protected override string ToStringImpl()
		{
			return $"{Button}";
		}
	}
}

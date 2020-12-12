﻿namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseProvider: AInputProvider
	{
		public EMouseButton Button;

		public MouseProvider(EMouseButton button)
		{
			Button = button;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			return inputManager.Mouse.Get(Button).ToRaw();
		}

		public override string ToString()
		{
			return string.Format("[MouseButton: {0}]", Button);
		}
	}
}

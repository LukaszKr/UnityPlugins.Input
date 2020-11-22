namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseScrollProvider: AInputProvider
	{
		public float MinValue;

		public MouseScrollProvider(float minValue)
		{
			MinValue = minValue;
		}

		protected override InputProviderData OnRefresh(InputManager inputManager)
		{
			float axis = inputManager.Mouse.Scroll.y;
			if(MinValue < 0)
			{
				return new InputProviderData(axis <= MinValue, axis);
			}
			else
			{
				return new InputProviderData(axis >= MinValue, axis);
			}
		}

		public override string ToString()
		{
			return string.Format("[MinValue: {0}]", MinValue.ToString());
		}
	}
}

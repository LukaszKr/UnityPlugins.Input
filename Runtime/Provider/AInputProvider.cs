namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputProvider
	{
		public RawInputState Refresh(InputManager inputManager)
		{
			return OnRefresh(inputManager);
		}

		protected abstract RawInputState OnRefresh(InputManager inputManager);
	}
}

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputProvider
	{
		public InputProviderState Refresh(InputManager inputManager)
		{
			return OnRefresh(inputManager);
		}

		protected abstract InputProviderState OnRefresh(InputManager inputManager);
	}
}

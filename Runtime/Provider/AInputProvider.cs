namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputProvider
	{
		public InputProviderData Refresh(InputManager inputManager)
		{
			return OnRefresh(inputManager);
		}

		protected abstract InputProviderData OnRefresh(InputManager inputManager);
	}
}

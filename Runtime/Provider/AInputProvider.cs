namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputProvider
	{
		public RawInputState GetState(InputManager inputManager)
		{
			return OnGetState(inputManager);
		}

		protected abstract RawInputState OnGetState(InputManager inputManager);
	}
}

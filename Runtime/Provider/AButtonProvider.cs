namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AButtonProvider: AInputProvider
	{
		private int m_UpdateTick = 0;

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			int newUpdateTick = inputManager.UpdateTick;
			if(newUpdateTick-1 != m_UpdateTick)
			{
				return new RawInputState(false);
			}

			m_UpdateTick = newUpdateTick;
			return GetInputStatus(inputManager);
		}

		protected abstract RawInputState GetInputStatus(InputManager inputManager);
	}
}

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputProvider
	{
		private int m_UpdateTick = 0;

		public RawInputState GetState(InputManager inputManager)
		{
			int oldTick = m_UpdateTick;
			m_UpdateTick = inputManager.UpdateTick;
			if(oldTick != m_UpdateTick-1)
			{
				return new RawInputState(false);
			}
			return OnGetState(inputManager);
		}

		protected abstract RawInputState OnGetState(InputManager inputManager);
	}
}

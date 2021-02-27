namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class ADetector
	{
		private int m_LastUpdateTick = 0;

		public abstract bool Triggered { get; }

		public void Update(AInputManager inputManager)
		{
			int updateTick = inputManager.UpdateTick;
			if(m_LastUpdateTick != updateTick)
			{
				m_LastUpdateTick = updateTick;
				OnUpdate(inputManager);
			}
		}

		public abstract void Validate(InputValidator resolver);

		protected abstract void OnUpdate(AInputManager inputManager);
	}
}

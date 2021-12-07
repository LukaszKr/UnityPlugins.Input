namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public abstract class ADetector
	{
		private int m_LastUpdateTick = 0;

		public abstract bool Triggered { get; }

		public void Update(int updateTick, float deltaTime)
		{
			if(m_LastUpdateTick != updateTick)
			{
				m_LastUpdateTick = updateTick;
				OnUpdate(updateTick, deltaTime);
			}
		}

		public abstract void Validate(InputValidator resolver);

		protected abstract void OnUpdate(int updateTick, float deltaTime);
	}
}

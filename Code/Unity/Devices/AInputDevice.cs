using System.Collections.Generic;

namespace UnityPlugins.Input.Unity
{
	public abstract class AInputDevice
	{
		private int m_LastUpdateTick;

		public bool Enabled = true;

		public abstract bool IsActive { get; }
		public abstract bool IsAnyKeyActive { get; }
		public abstract bool ShowCursor { get; }

		public void Update(int updateTick)
		{
			int delta = updateTick - m_LastUpdateTick;
			if(delta != 1)
			{
				OnSkippedFrame();
			}
			m_LastUpdateTick = updateTick;
			OnUpdate();
		}

		protected virtual void OnSkippedFrame()
		{

		}

		public abstract void ResetState();
		protected abstract void OnUpdate();

		public abstract void GetActiveProviders(List<AInputProvider> providers);
	}
}

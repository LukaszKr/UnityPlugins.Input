using System.Collections.Generic;

namespace ProceduralLevel.Input.Unity
{
	public abstract class ABaseInputDevice
	{
		public abstract bool IsActive { get; }
		public abstract bool IsAnyKeyActive { get; }

		public readonly EDeviceID ID;
		public bool Enabled = true;

		private int m_LastUpdateTick;

		public ABaseInputDevice(EDeviceID id)
		{
			ID = id;
		}

		public void UpdateState(int updateTick)
		{
			if(m_LastUpdateTick+1 != updateTick)
			{
				OnSkippedFrame();
			}
			m_LastUpdateTick = updateTick;
			OnUpdateState();
		}

		public abstract void ResetState();
		protected virtual void OnSkippedFrame() { }

		protected abstract void OnUpdateState();

		public abstract void GetActiveProviders(List<AInputProvider> providers);
	}
}

using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public abstract class AInputDevice
	{
		protected InputState[] m_InputState;

		protected bool m_IsActive;

		public bool IsActive => m_IsActive;

		public readonly EDeviceID ID;
		public bool Enabled = true;

		private int m_LastUpdateTick;

		public AInputDevice(EDeviceID id, int inputCount)
		{
			ID = id;
			m_InputState = new InputState[inputCount];
		}

		public InputState[] GetAllInputState()
		{
			return m_InputState;
		}

		public void UpdateState(int updateTick)
		{
			m_IsActive = false;

			if(m_LastUpdateTick+1 != updateTick)
			{
				OnSkippedFrame();
			}
			m_LastUpdateTick = updateTick;
			OnUpdateState();

			int length = m_InputState.Length;
			for(int rawInputID = 0; rawInputID < length; ++rawInputID)
			{
				InputState rawInput = GetRawState(rawInputID);
				m_InputState[rawInputID] = rawInput;
				m_IsActive |= rawInput.IsActive;
			}
		}

		public virtual void ResetState()
		{
			m_IsActive = false;

			int length = m_InputState.Length;
			for(int x = 0; x < length; ++x)
			{
				m_InputState[x] = default;
			}
		}

		protected virtual void OnSkippedFrame() { }

		protected abstract void OnUpdateState();
		protected abstract InputState GetRawState(int rawInputID);

		public abstract void RecordProviders(List<AInputProvider> providers);
	}
}

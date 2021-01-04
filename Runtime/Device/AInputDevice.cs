using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputDevice
	{
		protected InputState[] m_InputState;

		protected bool m_IsActive;
		protected bool m_AnyInputActive;

		public bool IsActive { get { return m_IsActive; } }
		public bool AnyKeyPressed { get { return m_AnyInputActive; } }

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

		public void UpdateState(InputManager inputManager)
		{
			m_IsActive = false;
			m_AnyInputActive = false;

			int updateTick = inputManager.UpdateTick;
			if(m_LastUpdateTick+1 != updateTick)
			{
				OnSkippedFrame();
			}
			m_LastUpdateTick = updateTick;
			OnUpdateState(inputManager);

			int length = m_InputState.Length;
			for(int rawInputID = 0; rawInputID < length; ++rawInputID)
			{
				RawInputState rawInput = GetRawState(rawInputID);
				InputState currentState = m_InputState[rawInputID];
				InputState newState = currentState.GetNextState(rawInput);
				m_InputState[rawInputID] = newState;
				m_IsActive = m_IsActive || rawInput.IsActive || newState.Status == EInputStatus.JustReleased;
				m_AnyInputActive |= rawInput.IsActive;
			}
		}

		public virtual void ResetState()
		{
			m_IsActive = false;
			m_AnyInputActive = false;

			int length = m_InputState.Length;
			for(int x = 0; x < length; ++x)
			{
				m_InputState[x] = new InputState();
			}
		}

		protected virtual void OnSkippedFrame() { }

		protected abstract void OnUpdateState(InputManager inputManager);
		protected abstract RawInputState GetRawState(int rawInputID);

		public abstract void RecordProviders(List<AInputProvider> providers);
	}
}

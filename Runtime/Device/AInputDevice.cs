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
			for(int buttonID = 0; buttonID < length; ++buttonID)
			{
				RawInputState rawInput = GetRawState(buttonID);
				InputState currentState = m_InputState[buttonID];
				InputState newState = currentState.GetNextState(rawInput);
				m_InputState[buttonID] = newState;
				m_IsActive = m_IsActive || rawInput.IsActive || newState.Status == EInputStatus.JustReleased;
				m_AnyInputActive |= rawInput.IsActive;
			}
		}

		protected virtual void OnSkippedFrame() { }

		protected abstract void OnUpdateState(InputManager inputManager);
		protected abstract RawInputState GetRawState(int inputID);

		public abstract void GetActiveInputLinks(List<AInputLink> links);
	}
}

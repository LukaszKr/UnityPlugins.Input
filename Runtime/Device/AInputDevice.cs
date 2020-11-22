namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputDevice
	{
		protected EButtonState[] m_KeyStates;

		protected bool m_IsActive;
		protected bool m_AnyKeyPressed;

		public bool IsActive { get { return m_IsActive; } }
		public bool AnyKeyPressed { get { return m_AnyKeyPressed; } }

		public readonly DeviceID ID;
		public bool Enabled = true;

		private int m_LastUpdateTick;

		public AInputDevice(DeviceID id, int buttonCount)
		{
			ID = id;
			m_KeyStates = new EButtonState[buttonCount];
		}

		public EButtonState[] GetAllButtons()
		{
			return m_KeyStates;
		}

		public void UpdateState(InputManager inputManager)
		{
			m_IsActive = false;
			m_AnyKeyPressed = false;

			int updateTick = inputManager.UpdateTick;
			if(m_LastUpdateTick+1 != updateTick)
			{
				OnSkippedFrame();
			}
			m_LastUpdateTick = updateTick;
			OnUpdateState(inputManager);

			int length = m_KeyStates.Length;
			for(int codeValue = 0; codeValue < length; ++codeValue)
			{
				bool isPressed = IsPressed(codeValue);
				EButtonState oldState = m_KeyStates[codeValue];
				EButtonState newState = oldState.GetNextState(isPressed);
				m_KeyStates[codeValue] = newState;

				m_IsActive = m_IsActive || isPressed || newState == EButtonState.JustReleased;
				m_AnyKeyPressed |= isPressed;
			}
		}

		protected virtual void OnSkippedFrame() { }

		protected abstract void OnUpdateState(InputManager inputManager);
		protected abstract bool IsPressed(int codeValue);
	}
}

namespace ProceduralLevel.Input.Unity
{
	public abstract class AInputDevice : ABaseInputDevice
	{
		protected bool m_IsActive;
		protected bool m_IsAnyKeyActive;
		protected InputState[] m_InputState;

		public override bool IsActive => m_IsActive;
		public override bool IsAnyKeyActive => m_IsAnyKeyActive;


		protected AInputDevice(EDeviceID id, int inputCount)
			: base(id)
		{
			m_InputState = new InputState[inputCount];
		}

		public InputState[] GetAllInputState()
		{
			return m_InputState;
		}

		protected override void OnUpdateState()
		{
			m_IsActive = false;
			m_IsAnyKeyActive = false;
			int length = m_InputState.Length;
			for(int rawInputID = 0; rawInputID < length; ++rawInputID)
			{
				InputState rawInput = GetState(rawInputID);
				m_InputState[rawInputID] = rawInput;
				m_IsActive |= rawInput.IsActive;
				m_IsAnyKeyActive |= rawInput.IsActive;
			}
		}

		protected abstract InputState GetState(int rawInputID);

		public override void ResetState()
		{
			m_IsActive = false;

			int length = m_InputState.Length;
			for(int x = 0; x < length; ++x)
			{
				m_InputState[x] = default;
			}
		}
	}
}

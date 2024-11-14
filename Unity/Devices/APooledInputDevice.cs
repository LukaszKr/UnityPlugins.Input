namespace UnityPlugins.Input.Unity
{
	public abstract class APooledInputDevice : AInputDevice
	{
		protected bool m_IsActive;
		protected bool m_IsAnyKeyActive;
		protected RawInputState[] m_InputState;

		public override bool IsActive => m_IsActive;
		public override bool IsAnyKeyActive => m_IsAnyKeyActive;

		protected APooledInputDevice(int inputCount)
		{
			m_InputState = new RawInputState[inputCount];
		}

		public RawInputState[] GetAllInputState()
		{
			return m_InputState;
		}

		protected override void OnUpdate()
		{
			m_IsActive = false;
			m_IsAnyKeyActive = false;
			int length = m_InputState.Length;
			for(int rawInputID = 0; rawInputID < length; ++rawInputID)
			{
				RawInputState rawInput = GetState(rawInputID);
				m_InputState[rawInputID] = rawInput;
				m_IsActive |= rawInput.IsActive;
				m_IsAnyKeyActive |= rawInput.IsActive;
			}
		}

		protected abstract RawInputState GetState(int rawInputID);

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

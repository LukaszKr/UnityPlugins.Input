namespace UnityPlugins.Input.Unity
{
	public abstract class AInputDetector : IGroupProvider
	{
		public readonly AnyProvider Group = new AnyProvider();

		private int m_LastUpdateTick = 0;
		private InputState m_InputState;

		private bool m_IsInputActive;
		private bool m_IsActive;

		public InputState InputState => m_InputState;
		public bool IsActive => m_IsActive;
		public float Axis => m_InputState.Axis;

		public bool Enabled = true;

		public void Update(int updateTick, float deltaTime)
		{
			if(!Enabled) //disabled detector should not update
			{
				if(m_IsInputActive)
				{
					ResetState();
				}
				return;
			}

			int delta = updateTick-m_LastUpdateTick;
			Group.Update(updateTick);
			m_InputState = Group.State;
			bool wasActive = m_IsInputActive;
			m_IsInputActive = m_InputState.IsActive;

			if(m_IsInputActive)
			{
				if(delta != 1) //there was more than 1 update tick since last update, disable.
				{
					ResetState();
					return;
				}
				m_IsActive = OnInputUpdate(m_InputState, deltaTime);
				if(!m_IsActive)
				{
					m_InputState = default;
				}
			}
			else if(wasActive)
			{
				ResetState();
			}

			m_LastUpdateTick = updateTick;
		}

		protected void ResetState()
		{
			m_InputState = default;
			m_IsActive = false;
			m_IsInputActive = false;
			OnResetState();
		}

		protected abstract bool OnInputUpdate(InputState inputState, float deltaTime);
		protected abstract void OnResetState();

		#region Providers
		public void Add(AInputProvider provider)
		{
			Group.Add(provider);
		}

		public void Sort()
		{
			Group.Sort();
		}
		#endregion

		public override string ToString()
		{
			return $"[{nameof(IsActive)}: {IsActive}, {nameof(Axis)}: {Axis}, {nameof(Group)}: {Group}]";
		}
	}
}

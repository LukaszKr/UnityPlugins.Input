namespace ProceduralLevel.Input.Unity
{
	public abstract class AInputDetector : IProviderContainer
	{
		public readonly GroupProvider Group = new GroupProvider();

		private int m_LastUpdateTick = 0;
		private InputState m_InputState;
		private bool m_Active;
		private bool m_ActivatedThisFrame;
		private bool m_DeactivatedThisFrame;
		private bool m_WasInputActive;

		public bool Active => m_Active;
		public bool ActivatedThisFrame => m_ActivatedThisFrame;
		public bool DeactivatedThisFrame => m_DeactivatedThisFrame;
		public float Axis => m_InputState.Axis;

		public bool Enabled = true;

		public void Update(int updateTick, float deltaTime)
		{
			if(m_LastUpdateTick != updateTick)
			{
				m_LastUpdateTick = updateTick;
				OnUpdate(updateTick, deltaTime);
			}
		}

		private void OnUpdate(int updateTick, float deltaTime)
		{
			m_DeactivatedThisFrame = false;
			if(!Enabled)
			{
				if(m_Active)
				{
					ResetState();
				}
				return;
			}
			m_InputState = Group.UpdateState(updateTick);

			if(m_InputState.IsActive)
			{
				m_WasInputActive = true;
				bool oldActive = m_Active;
				m_Active = OnInputUpdate(m_InputState, deltaTime);
				m_ActivatedThisFrame = (!oldActive && m_Active);
			}
			else if(m_WasInputActive)
			{
				ResetState();
			}
		}

		public void Validate(InputValidator resolver)
		{
			if(resolver.IsBlocked(this))
			{
				ResetState();
			}
		}

		private void ResetState()
		{
			m_WasInputActive = false;
			m_DeactivatedThisFrame = true;
			m_Active = false;
			OnInputReset();
		}

		protected abstract bool OnInputUpdate(InputState inputState, float deltaTime);
		protected abstract void OnInputReset();

		#region Providers
		public void AddProvider(AInputProvider provider)
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
			return $"[{nameof(Active)}: {Active}, {nameof(Axis)}: {Axis}, {nameof(Group)}: {Group}]";
		}
	}
}

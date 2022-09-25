namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public abstract class AInputDetector : IProviderContainer
	{
		public readonly GroupProvider Group = new GroupProvider();

		private int m_LastUpdateTick = 0;
		private RawInputState m_InputState;
		private bool m_Triggered;

		public RawInputState InputState => m_InputState;
		public bool Triggered => m_Triggered;
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
			if(!Enabled)
			{
				if(m_Triggered)
				{
					ResetState();
				}
				return;
			}
			m_InputState = Group.UpdateState(updateTick);

			if(m_InputState.IsActive)
			{
				m_Triggered = OnInputUpdate(deltaTime);
			}
			else
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
			OnInputReset();
			m_Triggered = false;
		}

		protected abstract bool OnInputUpdate(float deltaTime);
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
			return string.Format("[Triggered: {0}, Axis: {1}, InputProviders: {2}]",
				Triggered.ToString(), Axis.ToString(), Group.ToString());
		}
	}
}

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputDetector : ADetector, IProviderContainer
	{
		public readonly GroupProvider Group = new GroupProvider();

		private RawInputState m_InputState;
		private bool m_Triggered;

		public RawInputState InputState { get { return m_InputState; } }
		public override bool Triggered { get { return m_Triggered; } }
		public float Axis { get { return m_InputState.Axis; } }

		public bool Enabled = true;

		protected override void OnUpdate(int updateTick)
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
				m_Triggered = OnInputUpdate();
			}
			else
			{
				ResetState();
			}
		}

		public override void Validate(InputValidator resolver)
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

		protected abstract bool OnInputUpdate();
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

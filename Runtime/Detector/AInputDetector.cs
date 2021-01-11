namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputDetector: ADetector, IProviderContainer
	{
		public readonly GroupProvider Group = new GroupProvider();

		private RawInputState m_InputState;
		private bool m_Triggered;

		public RawInputState InputState { get { return m_InputState; } }
		public override bool Triggered { get { return m_Triggered; } }
		public float Axis { get { return m_InputState.Axis; } }

		protected override void OnUpdate(InputManager inputManager)
		{
			m_InputState = Group.UpdateState(inputManager);

			if(m_InputState.IsActive)
			{
				m_Triggered = OnInputUpdate(inputManager);
			}
			else
			{
				OnInputReset();
				m_Triggered = false;
			}
		}

		public override void Validate(InputValidator resolver)
		{
			if(resolver.IsBlocked(this))
			{
				OnInputReset();
				m_Triggered = false;
			}
		}

		protected abstract bool OnInputUpdate(InputManager inputManager);
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

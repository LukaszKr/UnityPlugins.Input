namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputDetector: ADetector, IProviderContainer
	{
		public readonly GroupProvider Providers = new GroupProvider();

		private RawInputState m_InputState;
		private bool m_Triggered;

		public RawInputState InputState { get { return m_InputState; } }
		public override bool Triggered { get { return m_Triggered; } }
		public float Axis { get { return m_InputState.Axis; } }

		protected override void OnUpdate(InputManager inputManager)
		{
			m_InputState = Providers.GetState(inputManager);
			if(m_InputState.IsActive)
			{
				m_Triggered = OnInputUpdate(inputManager);
			}
			else
			{
				OnInputReset(inputManager);
				m_Triggered = false;
			}
		}

		protected abstract bool OnInputUpdate(InputManager inputManager);
		protected abstract void OnInputReset(InputManager inputManager);

		#region Providers
		public void AddProvider(AInputProvider provider)
		{
			Providers.Add(provider);
		}

		public AInputDetector SetGroupMode(EProviderListMode mode)
		{
			Providers.Mode = mode;
			return this;
		}
		#endregion

		public override string ToString()
		{
			return string.Format("[Triggered: {0}, Axis: {1}, InputProviders: {2}]",
				Triggered.ToString(), Axis.ToString(), Providers.ToString());
		}
	}
}

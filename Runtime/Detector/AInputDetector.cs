using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputDetector: ADetector
	{
		public readonly List<AInputProvider> Providers = new List<AInputProvider>(1);

		private bool m_Triggered;

		private RawInputState m_InputState;

		public RawInputState InputState { get { return m_InputState; } }
		public override bool Triggered { get { return m_Triggered; } }
		public float Axis { get { return m_InputState.Axis; } }

		protected override void OnUpdate(InputManager inputManager)
		{
			bool anyProviderValid = false;
			int count = Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = Providers[x];
				RawInputState data = provider.GetState(inputManager);
				if(data.IsActive)
				{
					m_Triggered = OnInputUpdate(inputManager);
					anyProviderValid = true;
					m_InputState = data;
				}
			}

			if(!anyProviderValid)
			{
				m_InputState = new RawInputState();
				m_Triggered = false;
				OnInputReset(inputManager);
			}
		}

		protected abstract bool OnInputUpdate(InputManager inputManager);
		protected abstract void OnInputReset(InputManager inputManager);

		public override string ToString()
		{
			return string.Format("[Triggered: {0}, Axis: {1}, InputProviders: {2}]",
				Triggered.ToString(), Axis.ToString(), Providers.ToString());
		}
	}
}

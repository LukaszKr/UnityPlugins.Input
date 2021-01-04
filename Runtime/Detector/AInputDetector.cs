using System;
using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputDetector: ADetector, IProviderContainer
	{
		public readonly List<AInputProvider> Providers = new List<AInputProvider>();

		private RawInputState m_InputState;
		private bool m_Triggered;

		public RawInputState InputState { get { return m_InputState; } }
		public override bool Triggered { get { return m_Triggered; } }
		public float Axis { get { return m_InputState.Axis; } }

		protected override void OnUpdate(InputManager inputManager)
		{
			m_InputState = GetState(inputManager);
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

		private RawInputState GetState(InputManager inputManager)
		{
			float axis = 0f;
			bool isRealAxis = false;
			bool isAnyProviderActive = false;

			int count = Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = Providers[x];
				RawInputState data = provider.GetState(inputManager);
				if(data.IsActive)
				{
					isAnyProviderActive = true;

					if(data.IsRealAxis)
					{
						isRealAxis = true;
						axis = Math.Max(data.Axis, axis);
					}
					else if(!isRealAxis)
					{
						axis = data.Axis;
					}
				}
			}

			return new RawInputState(isAnyProviderActive, axis, isRealAxis);
		}

		protected abstract bool OnInputUpdate(InputManager inputManager);
		protected abstract void OnInputReset(InputManager inputManager);

		#region Providers
		public void AddProvider(AInputProvider provider)
		{
			Providers.Add(provider);
		}
		#endregion

		public override string ToString()
		{
			return string.Format("[Triggered: {0}, Axis: {1}, InputProviders: {2}]",
				Triggered.ToString(), Axis.ToString(), Providers.ToString());
		}
	}
}

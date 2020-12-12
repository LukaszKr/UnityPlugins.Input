using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputDetector: ADetector
	{
		private readonly List<AInputProvider> m_InputProviders = new List<AInputProvider>(1);

		private bool m_Triggered;

		private RawInputState m_InputState;

		public RawInputState InputState { get { return m_InputState; } }
		public override bool Triggered { get { return m_Triggered; } }
		public float Axis { get { return m_InputState.Axis; } }

		protected override void OnUpdate(InputManager inputManager)
		{
			bool anyProviderValid = false;
			int count = m_InputProviders.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_InputProviders[x];
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

		public AInputDetector Add(AInputProvider provider)
		{
			m_InputProviders.Add(provider);
			return this;
		}

		public AInputDetector Add(Key key)
		{
			m_InputProviders.Add(new KeyboardProvider(key));
			return this;
		}

		public AInputDetector Add(EMouseButton button)
		{
			m_InputProviders.Add(new MouseProvider(button));
			return this;
		}

		public AInputDetector Add(EGamepadButton button, EGamepadID gamepadID = EGamepadID.Any)
		{
			m_InputProviders.Add(new GamepadProvider(button, gamepadID));
			return this;
		}

		public AInputDetector Add(EGamepadButton button, GamepadDevice gamepad = null)
		{
			m_InputProviders.Add(new GamepadProvider(button, gamepad));
			return this;
		}

		public AInputDetector Add(ETouchID touchID)
		{
			m_InputProviders.Add(new TouchProvider(touchID));
			return this;
		}

		public AInputDetector AddTouchCount(int count)
		{
			m_InputProviders.Add(new TouchCountProvider(count));
			return this;
		}

		protected abstract bool OnInputUpdate(InputManager inputManager);
		protected abstract void OnInputReset(InputManager inputManager);

		public override string ToString()
		{
			return string.Format("[Triggered: {0}, Axis: {1}, InputProviders: {2}]", 
				Triggered.ToString(), Axis.ToString(), m_InputProviders.ToString());
		}
	}
}

using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputDetector: ADetector
	{
		private readonly List<AInputProvider> m_InputProviders = new List<AInputProvider>(1);

		private bool m_Triggered;
		private float m_Axis;

		public override bool Triggered { get { return m_Triggered; } }
		public float Axis { get { return m_Axis; } }

		protected override void OnUpdate(InputManager inputManager)
		{
			m_Axis = 0f;
			bool anyProviderValid = false;
			int count = m_InputProviders.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_InputProviders[x];
				InputProviderState data = provider.Refresh(inputManager);
				if(data.Triggered)
				{
					m_Triggered = OnInputUpdate(inputManager);
					anyProviderValid = true;
					m_Axis = data.Axis;
				}
			}
			
			if(!anyProviderValid)
			{
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
			m_InputProviders.Add(new KeyboardKeyProvider(key));
			return this;
		}

		public AInputDetector Add(EMouseButton button)
		{
			m_InputProviders.Add(new MouseButtonProvider(button));
			return this;
		}

		public AInputDetector Add(EGamepadButton button, EGamepadID gamepadID = EGamepadID.Any)
		{
			m_InputProviders.Add(new GamepadButtonProvider(button, gamepadID));
			return this;
		}

		public AInputDetector Add(EGamepadButton button, GamepadDevice gamepad = null)
		{
			m_InputProviders.Add(new GamepadButtonProvider(button, gamepad));
			return this;
		}

		public AInputDetector Add(EGamepadButton axis, float minValue, EGamepadID gamepadID = EGamepadID.Any)
		{
			m_InputProviders.Add(new GamepadAxisProvider(axis, minValue, gamepadID));
			return this;
		}

		public AInputDetector Add(EGamepadButton axis, float minValue, GamepadDevice gamepad = null)
		{
			m_InputProviders.Add(new GamepadAxisProvider(axis, minValue, gamepad));
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

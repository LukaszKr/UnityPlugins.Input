using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class AnyGamepadDevice : AGamepadDevice
	{
		public static readonly AnyGamepadDevice Instance = new AnyGamepadDevice();

		private GamepadDevice m_ActiveGamepad;

		public override Gamepad UnityGamepad { get { return m_ActiveGamepad?.UnityGamepad; } }
		public override EGamepadType GamepadType { get { return (m_ActiveGamepad != null ? m_ActiveGamepad.GamepadType : EGamepadType.Generic); } }

		public AnyGamepadDevice()
			: base(EGamepadID.Any)
		{

		}

		#region Getters
		public override InputState Get(EGamepadInputID inputID)
		{
			if(m_ActiveGamepad != null)
			{
				return m_ActiveGamepad.Get(inputID);
			}
			return new InputState(EInputStatus.Released);
		}

		public override EInputStatus GetStatus(EGamepadInputID inputID)
		{
			if(m_ActiveGamepad != null)
			{
				return m_ActiveGamepad.GetStatus(inputID);
			}
			return EInputStatus.Released;
		}

		public override float GetAxis(EGamepadInputID inputID)
		{
			if(m_ActiveGamepad != null)
			{
				return m_ActiveGamepad.GetAxis(inputID);
			}
			return 0f;
		}
		#endregion

		#region Update State
		protected override void OnUpdateState()
		{
			GamepadDevice[] gamepads = GamepadDevice.Gamepads;
			int length = gamepads.Length;
			for(int x = 0; x < length; ++x)
			{
				GamepadDevice device = gamepads[x];
				if(device.IsActive)
				{
					m_ActiveGamepad = device;
					break;
				}
			}
			if(m_ActiveGamepad != null)
			{
				m_IsActive = m_ActiveGamepad.IsActive;
			}
		}

		protected override RawInputState GetRawState(int rawInputID)
		{
			if(m_ActiveGamepad != null)
			{
				InputState state = m_ActiveGamepad.Get((EGamepadInputID)rawInputID);
				return new RawInputState(state);
			}
			return new RawInputState(false);
		}
		#endregion

		public override void Rumble(float low, float high)
		{
			if(m_ActiveGamepad != null)
			{
				m_ActiveGamepad.Rumble(low, high);
			}
		}

		public override void RecordProviders(List<AInputProvider> providers)
		{
			if(IsActive)
			{
				int length = m_InputState.Length;
				for(int x = 0; x < length; ++x)
				{
					InputState state = m_InputState[x];
					if(state.IsActive)
					{
						providers.Add(new GamepadProvider((EGamepadInputID)x));
					}
				}
			}
		}
	}
}

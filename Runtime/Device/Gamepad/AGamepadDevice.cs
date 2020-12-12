using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AGamepadDevice: AInputDevice
	{
		public readonly EGamepadID GamepadID;

		public abstract Gamepad UnityGamepad { get; }
		public abstract EGamepadType GamepadType { get; }

		public AGamepadDevice(EGamepadID gamepadID) : base(EDeviceID.Gamepad, EGamepadButtonExt.MAX_VALUE+1)
		{
			GamepadID = gamepadID;
		}

		public abstract EButtonState Get(EGamepadButton button);
		public abstract float GetAxis(EGamepadButton button);
		public abstract void Rumble(float low, float high);

		public override void GetActiveInputLinks(List<AInputLink> links)
		{
			if(IsActive)
			{
				int length = m_KeyStates.Length;
				for(int x = 0; x < length; ++x)
				{
					EButtonState state = m_KeyStates[x];
					if(EButtonState.IsDown.Contains(state))
					{
						links.Add(new GamepadInputLink((EGamepadButton)x));
					}
				}
			}
		}
	}
}

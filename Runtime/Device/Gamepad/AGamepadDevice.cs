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

		public abstract InputState Get(EGamepadButton button);
		public abstract EInputStatus GetStatus(EGamepadButton button);
		public abstract float GetAxis(EGamepadButton button);
		public abstract void Rumble(float low, float high);

		public override void GetActiveInputLinks(List<AInputLink> links)
		{
			if(IsActive)
			{
				int length = m_InputState.Length;
				for(int x = 0; x < length; ++x)
				{
					InputState state = m_InputState[x];
					if(state.IsActive)
					{
						links.Add(new GamepadInputLink((EGamepadButton)x));
					}
				}
			}
		}
	}
}

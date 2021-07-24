using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AGamepadDevice : AInputDevice
	{
		public readonly EGamepadID GamepadID;

		public abstract Gamepad UnityGamepad { get; }
		public abstract EGamepadType GamepadType { get; }

		public AGamepadDevice(EGamepadID gamepadID)
			: base(EDeviceID.Gamepad, EGamepadInputIDExt.Meta.MaxValue+1)
		{
			GamepadID = gamepadID;
		}

		public abstract InputState Get(EGamepadInputID inputID);
		public abstract EInputStatus GetStatus(EGamepadInputID inputID);
		public abstract float GetAxis(EGamepadInputID inputID);

		public abstract void Rumble(float low, float high);
	}
}

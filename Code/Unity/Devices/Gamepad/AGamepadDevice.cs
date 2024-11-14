using UnityEngine.InputSystem;

namespace UnityPlugins.Input.Unity
{
	public abstract class AGamepadDevice : APooledInputDevice
	{
		public readonly EGamepadID GamepadID;

		public abstract Gamepad UnityGamepad { get; }
		public abstract EGamepadType GamepadType { get; }

		public override bool ShowCursor => false;

		public AGamepadDevice(EGamepadID gamepadID)
			: base(EGamepadInputIDExt.Meta.MaxValue+1)
		{
			GamepadID = gamepadID;
		}

		public abstract RawInputState Get(EGamepadInputID inputID);
		public abstract float GetAxis(EGamepadInputID inputID);

		public abstract void Rumble(float low, float high);
	}
}

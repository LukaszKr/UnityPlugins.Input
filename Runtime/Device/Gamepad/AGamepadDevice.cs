using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AGamepadDevice: AInputDevice
	{
		public abstract Gamepad UnityGamepad { get; }
		public abstract EGamepadType GamepadType { get; }

		public AGamepadDevice() : base(DeviceID.Gamepad, EGamepadButtonExt.MAX_VALUE+1)
		{
		}

		public abstract EButtonState Get(EGamepadButton button);
		public abstract float GetAxis(EGamepadButton button);
		public abstract void Rumble(float low, float high);

	}
}

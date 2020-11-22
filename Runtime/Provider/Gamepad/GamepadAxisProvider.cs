using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class GamepadAxisProvider: AInputProvider
	{
		public EGamepadButton Axis;
		public float MinValue;
		public AGamepadDevice Gamepad;

		public GamepadAxisProvider(EGamepadButton axis, float minValue, AGamepadDevice gamepad = null)
		{
			if(minValue < 0 || minValue > 1)
			{
				throw new NotSupportedException("Min Value is not in range [0, 1]");
			}
			Axis = axis;
			MinValue = minValue;
			Gamepad = gamepad;
		}

		protected override InputProviderData OnRefresh(InputManager inputManager)
		{
			AGamepadDevice gamepad = (Gamepad == null? inputManager.AnyGamepad: Gamepad);
			float axis = gamepad.GetAxis(Axis);
			bool triggered = (axis >= MinValue);
			return new InputProviderData(triggered, axis);
		}

		public override string ToString()
		{
			return string.Format("[Axis: {0}, GamepadID: {1}, MinValue: {2}]", Axis.ToString(), Gamepad.ID.ToString(), MinValue.ToString());
		}
	}
}

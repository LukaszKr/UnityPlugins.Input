﻿using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class GamepadAxisProvider: AInputProvider
	{
		public EGamepadButton Axis;
		public float MinValue;
		public EGamepadID GamepadID;

		public GamepadAxisProvider(EGamepadButton axis, float minValue, EGamepadID gamepadID = EGamepadID.Any)
		{
			if(minValue < 0 || minValue > 1)
			{
				throw new NotSupportedException("Min Value is not in range [0, 1]");
			}
			Axis = axis;
			MinValue = minValue;
			GamepadID = gamepadID;
		}

		public GamepadAxisProvider(EGamepadButton axis, float minValue, AGamepadDevice gamepad = null)
		{
			if(minValue < 0 || minValue > 1)
			{
				throw new NotSupportedException("Min Value is not in range [0, 1]");
			}
			Axis = axis;
			MinValue = minValue;
			GamepadID = gamepad.GamepadID;
		}

		protected override RawInputState OnRefresh(InputManager inputManager)
		{
			AGamepadDevice gamepad = inputManager.GetGamepad(GamepadID);
			float axis = gamepad.GetAxis(Axis);
			bool triggered = (axis >= MinValue);
			return new RawInputState(triggered, axis);
		}

		public override string ToString()
		{
			return string.Format("[Axis: {0}, GamepadID: {1}, MinValue: {2}]", Axis.ToString(), GamepadID.ToString(), MinValue.ToString());
		}
	}
}

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.XInput;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Input.Unity
{
	public enum EGamepadType : byte
	{
		Generic = 0,

		Xbox360 = 1,
		XboxOne = 2,

		DualShockPS3 = 3,
		DualShockPS4 = 4,
		DualShockPS5 = 5,

		SwitchPro = 6,
	}

	public static class EGamepadTypeExt
	{
		public static readonly EnumExt<EGamepadType> Meta = new EnumExt<EGamepadType>();

		public static bool IsXbox(this EGamepadType type)
		{
			return type == EGamepadType.Xbox360 || type == EGamepadType.XboxOne;
		}

		public static bool IsDualShock(this EGamepadType type)
		{
			return type == EGamepadType.DualShockPS3 || type == EGamepadType.DualShockPS4;
		}

		public static EGamepadType FromGamepad(Gamepad gamepad)
		{
			if(gamepad is DualShockGamepad)
			{
				if(gamepad is DualShock3GamepadHID)
				{
					return EGamepadType.DualShockPS3;
				}
				if(gamepad is DualShock4GamepadHID)
				{
					return EGamepadType.DualShockPS4;
				}
				if(gamepad is DualSenseGamepadHID)
				{
					return EGamepadType.DualShockPS5;
				}
				return EGamepadType.DualShockPS4;
			}
			if(gamepad is SwitchProControllerHID)
			{
				return EGamepadType.SwitchPro;
			}

			if(gamepad is IXboxOneRumble) //Doesn't seem to be working on Windows
			{
				return EGamepadType.XboxOne;
			}

			if(gamepad is XInputController)
			{
				return EGamepadType.Xbox360;
			}

			return EGamepadType.Generic;
		}
	}
}

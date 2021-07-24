using ProceduralLevel.Common.Ext;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EGamepadType : byte
	{
		Generic = 0,

		Xbox360 = 1,
		XboxOne = 2,

		DualShockPS3 = 3,
		DualShockPS4 = 4,

		SwitchPro = 5,
	}

	public static class EGamepadTypeExt
	{
		public const string XBOX360 = "XInputControllerWindows";
		public const string DUALSHOCK3 = "DualShock3GamepadHID";
		public const string DUALSHOCK4 = "DualShock4GamepadHID";
		public const string SWITCH_PRO = "SwitchProControllerHID";

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
			string name = gamepad.GetType().Name;
			if(gamepad is IXboxOneRumble) //Doesn't seem to be working on Windows
			{
				return EGamepadType.XboxOne;
			}
			if(name == XBOX360)
			{
				return EGamepadType.Xbox360;
			}
			if(name == SWITCH_PRO)
			{
				return EGamepadType.SwitchPro;
			}
			if(name == DUALSHOCK3)
			{
				return EGamepadType.DualShockPS3;
			}
			if(name == DUALSHOCK4)
			{
				return EGamepadType.DualShockPS4;
			}

			return EGamepadType.Generic;
		}
	}
}

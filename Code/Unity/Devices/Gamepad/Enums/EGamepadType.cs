using UnityEngine.InputSystem;
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
using UnityEngine.InputSystem.DualShock;
#endif
using UnityEngine.InputSystem.XInput;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Input.Unity
{
	public enum EGamepadType : byte
	{
		Generic = 0,

		Xbox360 = 1,
		XboxOne = 2,

		DualShockPS3 = 10,
		DualShockPS4 = 11,
		DualShockPS5 = 12,

		SwitchPro = 20,

		SteamDeck = 30,
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
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
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
#endif

#if UNITY_STANDALONE_WIN
			if(gamepad is UnityEngine.InputSystem.Switch.SwitchProControllerHID)
			{
				return EGamepadType.SwitchPro;
			}
#endif

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

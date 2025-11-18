using UnityEngine.InputSystem.LowLevel;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Input.Unity
{
	public enum EGamepadInputID : byte
	{
		None = 0,

		LStick = 1,
		RStick = 2,

		LB = 3,
		RB = 4,

		A = 5,
		B = 6,
		X = 7,
		Y = 8,

		Back = 9,
		Start = 10,

		DPadLeft = 11,
		DPadRight = 12,
		DPadUp = 13,
		DPadDown = 14,

		//Axes
		LStickLeft = 15,
		LStickRight = 16,
		LStickUp = 17,
		LStickDown = 18,

		RStickLeft = 19,
		RStickRight = 20,
		RStickUp = 21,
		RStickDown = 22,

		LTrigger = 23,
		RTrigger = 24
	}

	public static class EGamepadInputIDExt
	{
		public static readonly EnumExt<EGamepadInputID> Meta = new EnumExt<EGamepadInputID>();

		private static readonly GamepadButton[] m_Map = new GamepadButton[] {
			GamepadButton.LeftStick, GamepadButton.RightStick,
			GamepadButton.LeftShoulder, GamepadButton.RightShoulder,
			GamepadButton.A, GamepadButton.B, GamepadButton.X, GamepadButton.Y,
			GamepadButton.Select, GamepadButton.Start,
			GamepadButton.DpadLeft, GamepadButton.DpadRight, GamepadButton.DpadUp, GamepadButton.DpadDown
		};

		public static GamepadButton ToUnity(this EGamepadInputID inputID)
		{
			return m_Map[(int)inputID-1];
		}

		public static bool IsAxis(this EGamepadInputID inputID)
		{
			return inputID >= EGamepadInputID.LStickLeft;
		}
	}
}

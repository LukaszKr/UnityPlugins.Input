using ProceduralLevel.Common.Ext;
using UnityEngine.InputSystem.LowLevel;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EGamepadInputID : byte
	{
		LStick = 0,
		RStick = 1,

		LB = 2,
		RB = 3,

		A = 4,
		B = 5,
		X = 6,
		Y = 7,

		Back = 8,
		Start = 9,

		DPadLeft = 10,
		DPadRight = 11,
		DPadUp = 12,
		DPadDown = 13,

		//Axes
		LStickLeft = 14,
		LStickRight = 15,
		LStickUp = 16,
		LStickDown = 17,

		RStickLeft = 18,
		RStickRight = 19,
		RStickUp = 20,
		RStickDown = 21,

		LTrigger = 22,
		RTrigger = 23
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
			return m_Map[(int)inputID];
		}

		public static bool IsAxis(this EGamepadInputID inputID)
		{
			return inputID >= EGamepadInputID.LStickLeft;
		}
	}
}

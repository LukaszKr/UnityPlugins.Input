using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Input.Unity
{
	public enum EGamepadID : byte
	{
		P1 = 0,
		P2 = 1,
		P3 = 2,
		P4 = 3
	}

	public static class EGamepadIDExt
	{
		public static readonly EnumExt<EGamepadID> Meta = new EnumExt<EGamepadID>();

		public static AGamepadDevice GetGamepad(this EGamepadID id)
		{
			GamepadDevice[] gamepads = GamepadDevice.Gamepads;
			return gamepads[(int)id];
		}

		public static Gamepad GetUnityGamepad(this EGamepadID id)
		{
			ReadOnlyArray<Gamepad> gamepads = Gamepad.all;
			int count = gamepads.Count;
			int intID = (int)id-1;
			if(intID < 0 || count <= intID)
			{
				return null;
			}
			return gamepads[intID];
		}
	}
}

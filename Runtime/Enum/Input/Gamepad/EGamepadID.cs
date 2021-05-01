using ProceduralLevel.Common.Ext;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EGamepadID: byte
	{
		Any = 0,
		P1 = 1,
		P2 = 2,
		P3 = 3,
		P4 = 4
	}

	public static class EGamepadIDExt
	{
		public static readonly EnumExt<EGamepadID> Meta = new EnumExt<EGamepadID>();

		public static AGamepadDevice GetGamepad(this EGamepadID id)
		{
			if(id == EGamepadID.Any)
			{
				return AnyGamepadDevice.Instance;
			}

			GamepadDevice[] gamepads = GamepadDevice.Gamepads;
			return gamepads[(int)id-1];
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

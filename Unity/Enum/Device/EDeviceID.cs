using System;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.Input.Unity
{
	public enum EDeviceID : byte
	{
		Unknown = 0,
		Keyboard = 1,
		Mouse = 2,
		Touch = 3,
		Gamepad = 4,
	}

	public static class EDeviceIDExt
	{
		public static readonly EnumExt<EDeviceID> Meta = new EnumExt<EDeviceID>();

		public static EDeviceGroup ToGroup(this EDeviceID id)
		{
			switch(id)
			{
				case EDeviceID.Keyboard:
				case EDeviceID.Mouse:
					return EDeviceGroup.KeyboardAndMouse;
				case EDeviceID.Touch:
					return EDeviceGroup.Touch;
				case EDeviceID.Gamepad:
					return EDeviceGroup.Gamepad;
				default:
					throw new NotSupportedException();
			}
		}
	}
}

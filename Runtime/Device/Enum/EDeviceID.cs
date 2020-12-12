﻿using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EDeviceID
	{
		Unknown = 0,
		Keyboard = 1,
		Mouse = 2,
		Touch = 3,
		Gamepad = 4,
		SwitchGamepad = 5
	}

	public static class EDeviceIDExt
	{
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
				case EDeviceID.SwitchGamepad:
					return EDeviceGroup.Gamepad;
				default:
					throw new NotSupportedException();
			}
		}
	}
}

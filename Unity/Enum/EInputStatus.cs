﻿using System;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	[Flags]
	public enum EInputStatus : byte
	{
		Released = 1 << 0,
		JustPressed = 1 << 1,
		Pressed = 1 << 2,
		JustReleased = 1 << 3,

		IsDown = Pressed | JustPressed,
		IsUp = Released | JustReleased
	}

	public static class EInputStatusExt
	{
		public static readonly EnumExt<EInputStatus> Meta = new EnumExt<EInputStatus>();

		public static bool Contains(this EInputStatus status, EInputStatus other)
		{
			return other != 0 && (status & other) == other;
		}

		public static EInputStatus GetNextStatus(this EInputStatus current, bool isPressed)
		{
			if(isPressed)
			{
				if(current == EInputStatus.Released)
				{
					return EInputStatus.JustPressed;
				}
				return EInputStatus.Pressed;
			}
			else if(current == EInputStatus.Pressed)
			{
				return EInputStatus.JustReleased;
			}
			return EInputStatus.Released;
		}
	}
}
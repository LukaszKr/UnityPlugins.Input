using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	[Flags]
	public enum EInputStatus: byte
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

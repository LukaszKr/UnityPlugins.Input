using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	[Flags]
	public enum EButtonState
	{
		Released = 1 << 0,
		JustPressed = 1 << 1,
		Pressed = 1 << 2,
		JustReleased = 1 << 3,

		IsDown = Pressed | JustPressed,
		IsUp = Released | JustReleased
	}

	public static class EKeyStateExt
	{
		public static bool Contains(this EButtonState state, EButtonState other)
		{
			return (state & other) == other;
		}

		public static EButtonState GetNextState(this EButtonState current, bool isPressed)
		{
			if(isPressed)
			{
				if (current == EButtonState.Released)
				{
					return EButtonState.JustPressed;
				}
				return EButtonState.Pressed;
			}
			else if (current == EButtonState.Pressed)
			{
				return EButtonState.JustReleased;
			}
			return EButtonState.Released;
		}
	}
}

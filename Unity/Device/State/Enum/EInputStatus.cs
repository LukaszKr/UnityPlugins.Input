using System;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.Input.Unity
{
	public enum EInputStatus : byte
	{
		Released = 0,
		JustReleased = 1,
		JustPressed = 2,
		Pressed = 3,
	}

	public static class EInputStatusExt
	{
		public static readonly EnumExt<EInputStatus> Meta = new EnumExt<EInputStatus>();

		public static bool IsPressed(this EInputStatus status)
		{
			return status == EInputStatus.Pressed || status == EInputStatus.JustPressed;
		}

		public static EInputStatus GetNext(this EInputStatus current, bool isActive)
		{
			if(isActive)
			{
				switch(current)
				{
					case EInputStatus.Released:
						return EInputStatus.JustPressed;
					case EInputStatus.JustReleased:
						return EInputStatus.Released;
					case EInputStatus.JustPressed:
						return EInputStatus.Pressed;
					case EInputStatus.Pressed:
						return EInputStatus.Pressed;
				}
			}
			else
			{
				switch(current)
				{
					case EInputStatus.Released:
						return EInputStatus.Released;
					case EInputStatus.JustReleased:
						return EInputStatus.Released;
					case EInputStatus.JustPressed:
						return EInputStatus.Released;
					case EInputStatus.Pressed:
						return EInputStatus.JustReleased;
				}
			}
			throw new NotSupportedException($"{current}:{isActive}");
		}
	}
}

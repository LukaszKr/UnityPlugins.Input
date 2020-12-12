using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EGamepadID
	{
		Any = 0,
		P1 = 1,
		P2 = 2,
		P3 = 3,
		P4 = 4
	}

	public static class EGamepadIDExt
	{
		public static readonly EGamepadID[] Values = (EGamepadID[])Enum.GetValues(typeof(EGamepadID));
	}
}

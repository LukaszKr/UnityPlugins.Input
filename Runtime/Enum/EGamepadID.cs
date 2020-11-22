using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EGamepadID
	{
		P1 = 0,
		P2 = 1,
		P3 = 2,
		P4 = 3
	}

	public static class EGamepadIDExt
	{
		public readonly static EGamepadID[] Values = (EGamepadID[])Enum.GetValues(typeof(EGamepadID));
	}
}

using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EMouseButton
	{
		Left = 0,
		Right = 1,
		Middle = 2,
		Back = 3,
		Forward = 4
	}

	public static class EMouseButtonExt
	{
		public static readonly EMouseButton[] Values = (EMouseButton[])Enum.GetValues(typeof(EMouseButton));
	}
}

using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EMouseButton
	{
		Left = 0,
		Right = 1,
		Middle = 2,
		Back = 3,
		Forward = 4,
		ScrollForward = 5,
		ScrollBackward = 6,
		ScrollLeft = 7,
		ScrollRight = 8
	}

	public static class EMouseButtonExt
	{
		public const int MAX_VALUE = 8;
		public static readonly EMouseButton[] Values = (EMouseButton[])Enum.GetValues(typeof(EMouseButton));
	}
}

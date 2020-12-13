using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EMouseButton: byte
	{
		Left = 0,
		Right = 1,
		Middle = 2,
		Back = 3,
		Forward = 4,

		//Axes
		ScrollLeft = 5,
		ScrollRight = 6,
		ScrollForward = 7,
		ScrollBackward = 8
	}

	public static class EMouseButtonExt
	{
		public const int MAX_VALUE = 8;
		public static readonly EMouseButton[] Values = (EMouseButton[])Enum.GetValues(typeof(EMouseButton));

		public static bool IsAxis(this EMouseButton button)
		{
			return button >= EMouseButton.ScrollLeft;
		}
	}
}

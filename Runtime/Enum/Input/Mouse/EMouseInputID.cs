using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EMouseInputID: byte
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

	public static class EMouseInputIDExt
	{
		public const int MAX_VALUE = 8;
		public static readonly EMouseInputID[] Values = (EMouseInputID[])Enum.GetValues(typeof(EMouseInputID));

		public static bool IsAxis(this EMouseInputID button)
		{
			return button >= EMouseInputID.ScrollLeft;
		}
	}
}

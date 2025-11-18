using UnityPlugins.Common.Logic;

namespace UnityPlugins.Input.Unity
{
	public enum EMouseInputID : byte
	{
		None = 0,

		Left = 1,
		Right = 2,
		Middle = 3,
		Back = 4,
		Forward = 5,

		//Axes
		ScrollLeft = 6,
		ScrollRight = 7,
		ScrollForward = 8,
		ScrollBackward = 9,

		MoveLeft = 10,
		MoveRight = 11,
		MoveUp = 12,
		MoveDown = 13,
	}

	public static class EMouseInputIDExt
	{
		public static readonly EnumExt<EMouseInputID> Meta = new EnumExt<EMouseInputID>();

		public static bool IsScroll(this EMouseInputID inputID)
		{
			return inputID >= EMouseInputID.ScrollLeft && inputID <= EMouseInputID.ScrollBackward;
		}

		public static bool IsMove(this EMouseInputID inputID)
		{
			return inputID >= EMouseInputID.MoveLeft && inputID <= EMouseInputID.MoveDown;
		}
	}
}

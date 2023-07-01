using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.Input.Unity
{
	public enum EMouseInputID : byte
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
		ScrollBackward = 8,

		MoveLeft = 9,
		MoveRight = 10,
		MoveUp = 11,
		MoveDown = 12
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

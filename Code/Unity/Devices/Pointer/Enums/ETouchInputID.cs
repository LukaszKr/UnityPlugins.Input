using UnityPlugins.Common.Logic;

namespace UnityPlugins.Input.Unity
{
	public enum ETouchInputID : byte
	{
		None = 0,

		Touch01 = 1,
		Touch02 = 2,
		Touch03 = 3,
		Touch04 = 4,
		Touch05 = 5,
	}

	public static class ETouchInputIDExt
	{
		public static readonly EnumExt<ETouchInputID> Meta = new EnumExt<ETouchInputID>();
	}
}

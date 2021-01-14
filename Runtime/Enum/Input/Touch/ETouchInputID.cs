using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum ETouchInputID: byte
	{
		Touch01 = 0,
		Touch02 = 1,
		Touch03 = 2,
		Touch04 = 3,
		Touch05 = 4,
	}

	public static class ETouchInputIDExt
	{
		public static readonly EnumExt<ETouchInputID> Meta = new EnumExt<ETouchInputID>();
	}
}

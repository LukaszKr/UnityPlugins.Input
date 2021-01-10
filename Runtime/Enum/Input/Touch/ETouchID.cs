using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum ETouchID: byte
	{
		Touch01 = 0,
		Touch02 = 1,
		Touch03 = 2,
		Touch04 = 3,
		Touch05 = 4,
	}

	public static class ETouchIDExt
	{
		public static readonly EnumExt<ETouchID> Meta = new EnumExt<ETouchID>();
	}
}

using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input
{
	public enum EDeviceGroup : byte
	{
		KeyboardAndMouse = 0,
		Gamepad = 1,
		Touch = 2
	}

	public static class EDeviceGroupExt
	{
		public static readonly EnumExt<EDeviceGroup> Meta = new EnumExt<EDeviceGroup>();
	}
}

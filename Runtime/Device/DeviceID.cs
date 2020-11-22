using ProceduralLevel.Common.SimpleID;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class DeviceID: ID
	{
		public static readonly DeviceID Keyboard = new DeviceID(1, "Keyboard", EDeviceGroup.KeyboardAndMouse);
		public static readonly DeviceID Mouse = new DeviceID(2, "Mouse", EDeviceGroup.KeyboardAndMouse);
		public static readonly DeviceID Gamepad = new DeviceID(3, "Gamepad", EDeviceGroup.Gamepad);
		public static readonly DeviceID Touch = new DeviceID(4, "Touch", EDeviceGroup.Touch);

		private readonly static IDGroup<DeviceID> m_Group = new IDGroup<DeviceID>();

		public readonly EDeviceGroup Group;

		private DeviceID(uint value, string name, EDeviceGroup group) 
			: base(value, name)
		{
			Group = group;
		}

		public static DeviceID Create(uint id, string name, EDeviceGroup group)
		{
			DeviceID deviceID = new DeviceID(id, name, group);
			m_Group.RegisterID(deviceID);
			return deviceID;
		}
	}
}

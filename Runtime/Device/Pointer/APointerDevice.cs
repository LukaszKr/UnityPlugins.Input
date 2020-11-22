namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class APointerDevice: AInputDevice
	{
		public APointerDevice(DeviceID id, int buttonCount) 
			: base(id, buttonCount)
		{
		}
	}
}

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public abstract class APointerDevice : AInputDevice
	{
		public APointerDevice(EDeviceID id, int inputCount)
			: base(id, inputCount)
		{
		}
	}
}

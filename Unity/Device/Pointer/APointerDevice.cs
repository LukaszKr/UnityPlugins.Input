using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public abstract class APointerDevice : AInputDevice
	{
		public abstract Vector2 ScreenDelta { get; }
		public abstract Vector2 RawDelta { get; }
		public abstract Vector2 Delta { get; }

		public abstract Vector2 Position { get; }
		public abstract Vector2 Scroll { get; }

		public APointerDevice(EDeviceID id, int inputCount)
			: base(id, inputCount)
		{
		}
	}
}

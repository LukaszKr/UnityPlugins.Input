using UnityEngine;

namespace UnityPlugins.Input.Unity
{
	public abstract class APointerDevice : APooledInputDevice
	{
		public abstract Vector2 ScreenDelta { get; }
		public abstract Vector2 NormalizedDelta { get; }
		public abstract Vector2 Delta { get; }

		public abstract Vector2 Position { get; }
		public abstract Vector2 Scroll { get; }

		public APointerDevice(int inputCount)
			: base(inputCount)
		{
		}
	}
}

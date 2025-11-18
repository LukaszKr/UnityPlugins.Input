using System;
using UnityEngine.InputSystem;

namespace UnityPlugins.Input.Unity
{
	public abstract class AGamepadDevice : APooledInputDevice, IComparable<AGamepadDevice>
	{
		public abstract Gamepad UnityGamepad { get; }
		public abstract EGamepadType GamepadType { get; }
		protected abstract int CompareValue { get; }

		public override bool ShowCursor => false;

		public AGamepadDevice()
			: base(EGamepadInputIDExt.Meta.MaxValue+1)
		{
		}

		public abstract RawInputState Get(EGamepadInputID inputID);
		public abstract float GetAxis(EGamepadInputID inputID);

		public abstract void Rumble(float low, float high);

		public int CompareTo(AGamepadDevice other)
		{
			return CompareValue.CompareTo(other.CompareValue);
		}
	}
}

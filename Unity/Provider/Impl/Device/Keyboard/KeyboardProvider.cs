using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public class KeyboardProvider : ADeviceInputProvider
	{
		public Key InputID;

		public KeyboardProvider()
		{
		}

		public KeyboardProvider(Key inputID)
		{
			InputID = inputID;
		}

		protected override RawInputState GetState()
		{
			return KeyboardDevice.Instance.Get(InputID).ToRaw();
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			KeyboardProvider otherProvider = (KeyboardProvider)other;
			return InputID.CompareTo(otherProvider.InputID);
		}

		protected override string ToStringImpl()
		{
			return $"{InputID}";
		}
	}
}

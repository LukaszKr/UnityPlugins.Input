using UnityEngine.InputSystem;

namespace UnityPlugins.Input.Unity
{
	public class KeyboardProvider : ADeviceInputProvider<KeyboardProvider, Key>
	{
		public KeyboardProvider()
		{
		}

		public KeyboardProvider(Key inputID)
			: base(inputID)
		{
		}

		public override RawInputState GetRawState()
		{
			return KeyboardDevice.Instance.Get(m_InputID);
		}
	}
}

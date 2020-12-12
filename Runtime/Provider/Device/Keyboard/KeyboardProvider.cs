using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class KeyboardProvider: AInputProvider
	{
		public Key Key;

		public KeyboardProvider(Key key)
			: base(EDeviceID.Keyboard)
		{
			Key = key;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			return inputManager.Keyboard.Get(Key).ToRaw();
		}

		protected override string ToStringImpl()
		{
			return $"{Key}";
		}
	}
}

using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class KeyboardProvider: AButtonProvider
	{
		public Key Key;

		public KeyboardProvider(Key key)
		{
			Key = key;
		}

		protected override EInputStatus GetButtonState(InputManager inputManager)
		{
			return inputManager.Keyboard.Get(Key);
		}


		public override string ToString()
		{
			return string.Format("[Key: {0}]", Key);
		}
	}
}

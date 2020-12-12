using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class KeyboardInputLink: AInputLink
	{
		public readonly Key Key;

		public KeyboardInputLink(Key key) 
			: base(EDeviceID.Keyboard)
		{
			Key = key;
		}

		protected override string ToStringImpl()
		{
			return $"{Key}";
		}
	}
}

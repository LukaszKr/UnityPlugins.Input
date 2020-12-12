namespace ProceduralLevel.UnityPlugins.Input
{
	public class GamepadInputLink: AInputLink
	{
		public readonly EGamepadButton Button;

		public GamepadInputLink(EGamepadButton button) 
			: base(EDeviceID.Gamepad)
		{
			Button = button;
		}

		protected override string ToStringImpl()
		{
			return $"{Button}";
		}
	}
}

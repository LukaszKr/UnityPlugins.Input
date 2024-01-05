namespace ProceduralLevel.Input.Unity
{
	public class InputManagerComponent : AInputManagerComponent
	{
		private InputManager _manager;

		public override InputManager Manager => _manager;

		public InputManagerComponent()
		{
			_manager = new InputManager();
		}
	}
}

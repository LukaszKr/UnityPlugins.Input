namespace ProceduralLevel.UnityPlugins.Input.Unity
{
    public class InputManagerComponent : AInputManagerComponent<InputManager>
    {
        private InputManager _manager;

        public override InputManager Manager => _manager;

        public InputManagerComponent()
        {
            _manager = new InputManager();
        }
    }
}

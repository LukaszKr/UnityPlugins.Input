namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchCountProvider: AInputProvider
	{
		public int Count;

		public TouchCountProvider(int count)
		{
			Count = count;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			return new RawInputState(inputManager.Touch.Count == Count);
		}
	}
}

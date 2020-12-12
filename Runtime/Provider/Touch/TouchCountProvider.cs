namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchCountProvider: AInputProvider
	{
		public int Count;

		public TouchCountProvider(int count)
		{
			Count = count;
		}

		protected override InputProviderState OnRefresh(InputManager inputManager)
		{
			return new InputProviderState(inputManager.Touch.Count == Count);
		}
	}
}

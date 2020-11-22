namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchCountProvider: AInputProvider
	{
		public int Count;

		public TouchCountProvider(int count)
		{
			Count = count;
		}

		protected override InputProviderData OnRefresh(InputManager inputManager)
		{
			return new InputProviderData(inputManager.Touch.Count == Count);
		}
	}
}

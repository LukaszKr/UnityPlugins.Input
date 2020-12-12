namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchCountProvider: AInputProvider
	{
		public int Count;

		public TouchCountProvider(int count)
			: base(EDeviceID.Touch)
		{
			Count = count;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			return new RawInputState(inputManager.Touch.Count == Count);
		}

		protected override string ToStringImpl()
		{
			return $"{nameof(Count)}: {Count}";
		}
	}
}

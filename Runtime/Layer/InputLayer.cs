namespace ProceduralLevel.UnityPlugins.Input
{
	public sealed class InputLayer
	{
		public IInputReceiver Receiver;
		public LayerDefinition Definition;
		public bool IsActive;

		public InputLayer(IInputReceiver receiver, LayerDefinition definition)
		{
			Receiver = receiver;
			Definition = definition;
		}

		public override string ToString()
		{
			return string.Format("[IsActive: {0}, Definition: {1}]", IsActive, Definition);
		}
	}
}

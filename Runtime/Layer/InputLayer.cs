namespace ProceduralLevel.UnityPlugins.Input
{
	public sealed class InputLayer
	{
		public IInputReceiver Receiver;
		public readonly DetectorUpdater Updater;
		public LayerDefinition Definition;
		public bool IsActive;

		public InputLayer(IInputReceiver receiver, DetectorUpdater updater, LayerDefinition definition)
		{
			Receiver = receiver;
			Updater = updater;
			Definition = definition;
		}

		public override string ToString()
		{
			return string.Format("[IsActive: {0}, Definition: {1}]", IsActive, Definition);
		}
	}
}

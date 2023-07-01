namespace ProceduralLevel.Input.Unity
{
	public sealed class InputLayer
	{
		public IInputReceiver Receiver;
		public readonly DetectorUpdater Updater;
		public InputLayerDefinition Definition;
		public bool IsActive;

		public InputLayer(IInputReceiver receiver, DetectorUpdater updater, InputLayerDefinition definition)
		{
			Receiver = receiver;
			Updater = updater;
			Definition = definition;
		}

		public override string ToString()
		{
			return $"[{nameof(IsActive)}: {IsActive}, {nameof(Definition)}: {Definition}]";
		}
	}
}

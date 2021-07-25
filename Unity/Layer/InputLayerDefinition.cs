namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public readonly struct InputLayerDefinition
	{
		public readonly string DebugName;
		public readonly int Priority;
		public readonly bool Block;

		public InputLayerDefinition(string debugName, int priority, bool block)
		{
			DebugName = debugName;
			Priority = priority;
			Block = block;
		}

		public override string ToString()
		{
			return $"[{DebugName}, {nameof(Priority)}: {Priority}, {nameof(Block)}: {Block}]";
		}
	}
}

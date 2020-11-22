using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	[Serializable]
	public sealed class LayerDefinition
	{
		public int ID;
		public int Priority;
		public bool Block;

		public LayerDefinition(int id, int priority, bool block)
		{
			ID = id;
			Priority = priority;
			Block = block;
		}

		public override string ToString()
		{
			return string.Format("[ID: {0}, Priority: {1}, Block: {2}]", ID, Priority, Block);
		}
	}
}

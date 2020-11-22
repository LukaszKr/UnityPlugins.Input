using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input
{
	public struct TouchData
	{
		public readonly Vector2 Position;
		public readonly Vector2 Delta;

		public TouchData(Vector2 position, Vector2 delta)
		{
			Position = position;
			Delta = delta;
		}

		public override string ToString()
		{
			return $"(Position: {Position}, Delta: {Delta})";
		}
	}
}

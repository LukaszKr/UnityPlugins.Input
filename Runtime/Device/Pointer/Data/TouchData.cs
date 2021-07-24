using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input
{
	public struct TouchData
	{
		public readonly Vector2 Position;
		public readonly Vector2 ScreenDelta;
		public readonly Vector2 RawDelta;
		public readonly Vector2 Delta;

		public TouchData(Vector2 position, Vector2 screenDelta, Vector2 rawDelta, Vector2 delta)
		{
			Position = position;
			ScreenDelta = screenDelta;
			RawDelta = rawDelta;
			Delta = delta;
		}

		public override string ToString()
		{
			return $"(Position: {Position}, Delta: {Delta})";
		}
	}
}

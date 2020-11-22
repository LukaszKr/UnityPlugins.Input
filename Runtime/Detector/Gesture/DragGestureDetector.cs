using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class DragGestureDetector: AGestureDetector
	{
		private readonly int m_FingerCount;

		private Vector2 m_Position;
		private Vector2 m_Delta;

		public Vector2 Delta { get { return m_Delta; } }

		public DragGestureDetector(int fingerCount = 2)
		{
			m_FingerCount = fingerCount;
		}

		protected override bool IsGestureActive(TouchDevice touch)
		{
			return touch.Count == m_FingerCount;
		}

		protected override void StartGesture(TouchDevice touch)
		{
			m_Position = touch.Touches[0].Position;
		}

		protected override bool UpdateGesture(TouchDevice touch)
		{
			Vector2 newPosition = touch.Touches[0].Position;
			m_Delta = m_Position-newPosition;
			m_Position = newPosition;
			return true;
		}

		protected override void EndGesture(TouchDevice device)
		{
			m_Delta = new Vector2(0f, 0f);
		}
	}
}

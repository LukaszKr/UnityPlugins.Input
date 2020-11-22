using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class PinchGestureDetector: AGestureDetector
	{
		private readonly int m_FingerCount;

		private Vector2 m_PointA;
		private Vector2 m_PointB;
		private float m_Distance;

		public float Distance { get { return m_Distance; } }
		public float Delta { get; private set; }

		public PinchGestureDetector(int fingerCount = 2)
		{
			m_FingerCount = fingerCount;
		}

		protected override bool IsGestureActive(TouchDevice touch)
		{
			return touch.Count == m_FingerCount;
		}

		protected override void StartGesture(TouchDevice touch)
		{
			UpdatePoints(touch);
		}

		protected override bool UpdateGesture(TouchDevice touch)
		{
			float oldDistance = m_Distance;
			UpdatePoints(touch);
			Delta = oldDistance-m_Distance;
			return true;
		}

		protected override void EndGesture(TouchDevice device)
		{
			m_Distance = 0f;
			Delta = 0f;
		}

		private void UpdatePoints(TouchDevice touch)
		{
			m_PointA = touch.Touches[0].Position;
			m_PointB = touch.Touches[1].Position;
			m_Distance = (m_PointA-m_PointB).sqrMagnitude;
		}
	}
}

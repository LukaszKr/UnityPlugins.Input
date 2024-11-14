namespace UnityPlugins.Input.Unity
{
	public class IntervalDetector : DurationDetector
	{
		private float[] m_Intervals;
		private float m_NextTriggerAt;
		private int m_IntervalIndex;

		private float m_CurrentInterval;

		public float CurrentInterval => m_CurrentInterval;

		public IntervalDetector(params float[] intervals)
		{
			m_Intervals = new float[intervals.Length];
			for(int x = 0; x < m_Intervals.Length; ++x)
			{
				m_Intervals[x] = intervals[x];
			}

			Restart();
		}

		protected override bool OnInputUpdate(InputState inputState, float deltaTime)
		{
			base.OnInputUpdate(inputState, deltaTime);

			bool intervalHit = false;
			while(Duration >= m_NextTriggerAt)
			{
				intervalHit = true;
				UpdateInterval();
			}
			return intervalHit;
		}

		private void Restart()
		{
			m_IntervalIndex = 0;
			m_NextTriggerAt = m_Intervals[m_IntervalIndex];
			m_CurrentInterval = m_Intervals[m_IntervalIndex];
		}

		protected override void OnResetState()
		{
			base.OnResetState();

			Restart();
		}

		private void UpdateInterval()
		{
			if(m_IntervalIndex < m_Intervals.Length-1)
			{
				m_IntervalIndex++;
				m_CurrentInterval = m_Intervals[m_IntervalIndex];
			}
			m_NextTriggerAt += CurrentInterval;
		}

		public override string ToString()
		{
			string intervals = "";
			for(int x = 0; x < m_Intervals.Length; ++x)
			{
				if(x > 0)
				{
					intervals += ",";
				}
				intervals += m_Intervals[x].ToString();
			}

			return base.ToString()+$"[{nameof(CurrentInterval)}: {CurrentInterval}, Intervals: {intervals}]";
		}
	}
}

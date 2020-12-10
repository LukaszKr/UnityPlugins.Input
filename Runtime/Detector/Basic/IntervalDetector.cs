namespace ProceduralLevel.UnityPlugins.Input
{
	public class IntervalDetector: DurationDetector
	{
		private float[] m_Intervals;
		private float m_PreviousTrigger;
		private float m_NextTrigger;
		private int m_IntervalIndex;

		public float CurrentInterval { get; private set; }
		public float Count { get; private set; }
		public float Progress 
		{ 
			get 
			{ 
				float diff = m_NextTrigger-m_PreviousTrigger;
				if(diff > 0)
				{
					return (Duration-m_PreviousTrigger)/diff;
				}
				return 0f;
			}
		}

		public IntervalDetector(params float[] intervals)
		{
			m_Intervals = new float[intervals.Length];
			for(int x = 0; x < m_Intervals.Length; ++x)
			{
				m_Intervals[x] = intervals[x];
			}
		}

		protected override bool OnInputUpdate(InputManager inputManager)
		{
			base.OnInputUpdate(inputManager);

			bool updated = false;
			while(Duration >= m_NextTrigger)
			{
				updated = true;
				UpdateInterval();
			}
			return updated;
		}

		protected override void OnInputReset(InputManager inputManager)
		{
			base.OnInputReset(inputManager);

			m_PreviousTrigger = 0f;
			m_NextTrigger = m_Intervals[0];
			CurrentInterval = m_Intervals[0];
			m_IntervalIndex = 0;
			Count = 0;
		}

		private void UpdateInterval()
		{
			if(m_IntervalIndex < m_Intervals.Length-1)
			{
				m_IntervalIndex ++;
				CurrentInterval = m_Intervals[m_IntervalIndex];
			}
			m_PreviousTrigger = m_NextTrigger;
			m_NextTrigger += m_Intervals[m_IntervalIndex];
			Count ++;
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

			return base.ToString()+string.Format("[CurrentInterval: {0}, Count: {1}, Intervals: {2}]", CurrentInterval, Count, intervals);
		}
	}
}

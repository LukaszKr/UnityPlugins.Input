using System;

namespace ProceduralLevel.Input.Unity
{
	public abstract class DelayedDetector : DurationDetector
	{
		private bool m_Detected;

		public float Progress => Math.Max(Duration/Delay, 1f);
		public float Delay { get; private set; }

		public DelayedDetector(float delay)
		{
			Delay = delay;
		}

		protected override bool OnInputUpdate(float deltaTime)
		{
			base.OnInputUpdate(deltaTime);

			if(!m_Detected && Duration >= Delay)
			{
				m_Detected = true;
				return true;
			}
			return false;
		}

		protected override void OnInputReset()
		{
			base.OnInputReset();
			m_Detected = false;
		}

		public override string ToString()
		{
			return base.ToString()+$"[{nameof(m_Detected)}: {m_Detected}, {nameof(Progress)}: {Progress}, {nameof(Delay)}: {Delay}]";
		}
	}
}

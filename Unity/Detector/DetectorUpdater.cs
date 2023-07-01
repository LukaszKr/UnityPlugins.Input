using System.Collections.Generic;

namespace ProceduralLevel.Input.Unity
{
	public class DetectorUpdater
	{
		private List<AInputDetector> m_Detectors = new List<AInputDetector>();

		public IReadOnlyList<AInputDetector> Detectors => m_Detectors;

		public DetectorUpdater(params AInputDetector[] detectors)
		{
			m_Detectors.AddRange(detectors);
		}

		public void Add(params AInputDetector[] detectors)
		{
			m_Detectors.AddRange(detectors);
		}

		public bool Remove(AInputDetector detector)
		{
			return m_Detectors.Remove(detector);
		}

		public void Update(int updateTick, float deltaTime)
		{
			int count = m_Detectors.Count;
			for(int x = 0; x < count; ++x)
			{
				m_Detectors[x].Update(updateTick, deltaTime);
			}
		}

		public void Validate(InputValidator validator)
		{
			int count = m_Detectors.Count;
			for(int x = 0; x < count; ++x)
			{
				m_Detectors[x].Validate(validator);
			}
		}
	}
}

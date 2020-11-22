using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class DetectorUpdater
	{
		private List<ADetector> m_Detectors = new List<ADetector>();

		public DetectorUpdater(params ADetector[] detectors)
		{
			m_Detectors.AddRange(detectors);
		}

		public void Add(params ADetector[] detectors)
		{
			m_Detectors.AddRange(detectors);
		}

		public void Update(InputManager inputManager)
		{
			int count = m_Detectors.Count;
			for(int x = 0; x < count; ++x)
			{
				m_Detectors[x].Update(inputManager);
			}
		}
	}
}

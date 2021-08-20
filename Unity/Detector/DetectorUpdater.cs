﻿using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public class DetectorUpdater
	{
		private List<ADetector> m_Detectors = new List<ADetector>();

		public IReadOnlyList<ADetector> Detectors { get { return m_Detectors; } }

		public DetectorUpdater(params ADetector[] detectors)
		{
			m_Detectors.AddRange(detectors);
		}

		public void Add(params ADetector[] detectors)
		{
			m_Detectors.AddRange(detectors);
		}

		public void Update(int updateTick)
		{
			int count = m_Detectors.Count;
			for(int x = 0; x < count; ++x)
			{
				m_Detectors[x].Update(updateTick);
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
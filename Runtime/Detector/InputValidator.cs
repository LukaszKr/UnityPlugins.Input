using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class InputValidator
	{
		private const int BUFFER_LENGTH = 64;

		private readonly List<AInputDetector> m_Detectors = new List<AInputDetector>();
		private readonly List<DetectorUpdater> m_Updaters = new List<DetectorUpdater>();

		private int m_BufferLength = 0;
		private readonly AInputProvider[] m_ActiveBuffer = new AInputProvider[BUFFER_LENGTH];
		private bool m_ShouldWipeBuffer;

		public void Add(AInputDetector detector)
		{
			m_Detectors.Add(detector);
		}

		public void Remove(AInputDetector detector)
		{
			m_Detectors.Remove(detector);
			m_ShouldWipeBuffer = true;
		}

		public void Add(DetectorUpdater updater)
		{
			m_Updaters.Add(updater);
		}

		public void Remove(DetectorUpdater updater)
		{
			m_Updaters.Remove(updater);
			m_ShouldWipeBuffer = true;
		}

		public void Update()
		{
			TryWipeBuffer();

			m_BufferLength = 0;

			Update(m_Detectors);

			int updaterCount = m_Updaters.Count;
			for(int x = 0; x < updaterCount; ++x)
			{
				DetectorUpdater updater = m_Updaters[x];
				Update(updater.Detectors);
			}
		}

		private void Update(IReadOnlyList<ADetector> detectors)
		{
			int count = detectors.Count;
			for(int x = 0; x < count; ++x)
			{
				ADetector detector = detectors[x];
				if(!detector.Triggered)
				{
					continue;
				}

				AInputDetector inputDetector = detector as AInputDetector;
				if(inputDetector == null)
				{
					continue;
				}

				m_ActiveBuffer[m_BufferLength++] = inputDetector.Group.UsedProvider;
			}
		}

		private void TryWipeBuffer()
		{
			if(m_ShouldWipeBuffer)
			{
				m_ShouldWipeBuffer = false;
				for(int x = 0; x < BUFFER_LENGTH; ++x)
				{
					m_ActiveBuffer[x] = null;
				}
			}
		}

		public bool IsBlocked(AInputDetector inputDetector)
		{
			if(inputDetector.Triggered)
			{
				AInputProvider provider = inputDetector.Group.UsedProvider;
				for(int x = 0; x < m_BufferLength; ++x)
				{
					AInputProvider otherProvider = m_ActiveBuffer[x];
					if(otherProvider != provider)
					{
						if(otherProvider.Contains(provider))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}
}

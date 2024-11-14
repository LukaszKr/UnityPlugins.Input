using UnityPlugins.Input.Unity.Providers;
using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Detectors.Basic
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class IntervalDetectorTests : AInputDetectorTests<IntervalDetector>
	{
		private static readonly float[] m_DecreasingIntervals = new float[] { 0.2f, 0.15f, 0.1f };
		private static readonly float[] m_InstantIntervals = new float[] { 0f, 0.2f, 0.1f };

		[Test, Description("If first interval is 0, delta time can also be 0.")]
		public void FirstIntervalAtZero_ActivatesInstantly()
		{
			IntervalDetector detector = new IntervalDetector(m_InstantIntervals);
			detector.Add(new TestInputProvider(new RawInputState(true)));

			detector.Update(1, 0f);
			Assert.IsTrue(detector.IsActive);
		}

		[Test, Description("Each interval will take it's turn to be active.")]
		public void EachIntervalCanBeReached()
		{
			IntervalDetector detector = new IntervalDetector(m_DecreasingIntervals);
			detector.Add(new TestInputProvider(new RawInputState(true)));

			for(int x = 0; x < m_DecreasingIntervals.Length; ++x)
			{
				float interval = m_DecreasingIntervals[x];
				Assert.AreEqual(interval, detector.CurrentInterval);
				detector.Update(x+1, interval);
				Assert.IsTrue(detector.IsActive);
			}
		}

		[Test, Description("If detector gets hit with delta time higher than next few intervals, they will be skipped.")]
		public void LargeDeltaTime_SkipIntervals()
		{
			IntervalDetector detector = new IntervalDetector(m_DecreasingIntervals);
			detector.Add(new TestInputProvider(new RawInputState(true)));

			detector.Update(1, 10f);
			Assert.IsTrue(detector.IsActive);

			detector.Update(2, 0f);
			Assert.IsFalse(detector.IsActive);
		}

		[Test, Description("If delta time is smaller than intervals, detector will be reporting inactivity.")]
		public void DeltaSmallerThanInterval_DetectorCanGoInactive()
		{
			IntervalDetector detector = new IntervalDetector(m_DecreasingIntervals);
			detector.Add(new TestInputProvider(new RawInputState(true)));

			detector.Update(1, 0.1f);
			Assert.IsFalse(detector.IsActive);
		
			detector.Update(2, 0.1f);
			Assert.IsTrue(detector.IsActive);
			
			detector.Update(3, 0.1f);
			Assert.IsFalse(detector.IsActive);

			detector.Update(4, 0.1f);
			Assert.IsTrue(detector.IsActive);
		}
	}
}

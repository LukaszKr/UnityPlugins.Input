using UnityPlugins.Input.Unity.Providers;
using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Detectors.Basic
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class DurationDetectorTests : AInputDetectorTests<DurationDetector>
	{
		[Test]
		public void Duration_IsNotIncreasedWhenInactive()
		{
			DurationDetector detector = new DurationDetector();
			detector.Add(new TestInputProvider(new RawInputState(false)));

			detector.Update(1, 0.1f);
			Assert.AreEqual(0f, detector.Duration);
			Assert.IsFalse(detector.IsActive);
		}

		[Test]
		public void Duration_IncreaseWithDelta()
		{
			DurationDetector detector = new DurationDetector();
			detector.Add(new TestInputProvider(new RawInputState(true)));

			detector.Update(1, 0.1f);
			Assert.AreEqual(0.1f, detector.Duration);
			Assert.IsTrue(detector.IsActive);
		}

		[Test, Description("Duration should be stacking up.")]
		public void Duration_IsAdditive()
		{
			DurationDetector detector = new DurationDetector();
			detector.Add(new TestInputProvider(new RawInputState(true)));

			detector.Update(1, 0.1f);
			Assert.AreEqual(0.1f, detector.Duration);
			Assert.IsTrue(detector.IsActive);
			detector.Update(2, 0.15f);
			Assert.AreEqual(0.25f, detector.Duration);
		}

		[Test, Description("Duration should be restarted when detector becomes inactive.")]
		public void Duration_IsRestartedBetweenActivations()
		{
			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			DurationDetector detector = new DurationDetector();
			detector.Add(provider);

			detector.Update(1, 0.2f);
			Assert.AreEqual(0.2f, detector.Duration);
			Assert.IsTrue(detector.IsActive);
			
			provider.Result = new RawInputState(false);
			detector.Update(2, 0.1f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(0f, detector.Duration);

			provider.Result = new RawInputState(true);
			detector.Update(3, 0.25f);
			Assert.AreEqual(0.25f, detector.Duration);
			Assert.IsTrue(detector.IsActive);
		}
	}
}

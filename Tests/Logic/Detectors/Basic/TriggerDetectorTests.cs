using UnityPlugins.Input.Unity.Providers;
using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Detectors.Basic
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class TriggerDetectorTests : AInputDetectorTests<TriggerDetector>
	{
		[Test]
		public void Trigger_CanTrigger()
		{
			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			TriggerDetector detector = new TriggerDetector();
			detector.Add(provider);

			detector.Update(1, 0f);
			Assert.IsTrue(detector.IsActive);
			Assert.AreEqual(1f, detector.Axis);

			detector.Update(2, 0f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(0f, detector.Axis);
		}

		[Test]
		public void Trigger_CanTrigger_MultipleTimes()
		{
			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			TriggerDetector detector = new TriggerDetector();
			detector.Add(provider);

			detector.Update(1, 0f);
			Assert.IsTrue(detector.IsActive);
			Assert.AreEqual(1f, detector.Axis);

			provider.Result = new RawInputState(false);
			detector.Update(2, 0f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(0f, detector.Axis);

			provider.Result = new RawInputState(true);
			detector.Update(3, 0f);
			Assert.IsTrue(detector.IsActive);
			Assert.AreEqual(1f, detector.Axis);
		}
	}
}

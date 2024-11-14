using UnityPlugins.Input.Unity.Providers;
using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Detectors
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class InputDetectorTests
	{
		[Test]
		public void Disable_BeforeActive_ResetIsNotCalled()
		{
			TestDetector detector = new TestDetector();

			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			detector.Enabled = false;

			detector.Add(provider);
			detector.Update(1, 1f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(new InputState(), detector.InputState);
			Assert.AreEqual(0, detector.ResetStateCallCount);
			Assert.AreEqual(0, detector.OnInputUpdateCallCount);
		}

		[Test]
		public void Disable_WhileActive_ResetIsCalled()
		{
			TestDetector detector = new TestDetector();

			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			detector.Add(provider);
			detector.Update(1, 1f);
			Assert.IsTrue(detector.IsActive);
			Assert.AreNotEqual(new InputState(), detector.InputState);
			Assert.AreEqual(1, detector.OnInputUpdateCallCount);

			detector.Enabled = false;
			detector.Update(2, 1f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(new InputState(), detector.InputState);
			Assert.AreEqual(1, detector.ResetStateCallCount);

			detector.Update(3, 1f); //was not active, reset call not called anymore
			Assert.AreEqual(1, detector.ResetStateCallCount);
			Assert.AreEqual(1, detector.OnInputUpdateCallCount);
		}

		[Test]
		public void Update_ProviderBecameActiveAfter_Activate()
		{
			TestDetector detector = new TestDetector();
			TestInputProvider provider = new TestInputProvider(new RawInputState(false));
			detector.Add(provider);

			detector.Update(1, 0f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(0, detector.OnInputUpdateCallCount);

			provider.Result = new RawInputState(true);
			detector.Update(2, 0f);
			Assert.IsTrue(detector.IsActive);
			Assert.AreEqual(1, detector.OnInputUpdateCallCount);
		}

		[Test]
		public void Update_ProviderIsActive_Activate()
		{
			TestDetector detector = new TestDetector();
			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			detector.Add(provider);

			detector.Update(1, 0f);
			Assert.IsTrue(detector.IsActive);
			Assert.AreEqual(1, detector.OnInputUpdateCallCount);
		}

		[Test]
		public void Update_SkippedFrame_WhileActive_ResetIsCalled()
		{
			TestDetector detector = new TestDetector();
			detector.Add(new TestInputProvider(new RawInputState(true)));

			detector.Update(1, 1f);
			Assert.IsTrue(detector.IsActive);
			Assert.AreEqual(1, detector.OnInputUpdateCallCount);

			detector.Update(3, 1f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(1, detector.ResetStateCallCount);
		}

		[Test, Description("If we start updating detector while for example button is pressed, we don't want it to be active. This is for example when popup opens.")]
		public void Update_SkippedFrame_ProviderIsActive_DoNotActivate()
		{
			TestDetector detector = new TestDetector();
			detector.Add(new TestInputProvider(new RawInputState(true)));

			detector.Update(2, 0f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(0, detector.OnInputUpdateCallCount);
		}
	}
}

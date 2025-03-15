using UnityPlugins.Input.Unity.Providers;
using NUnit.Framework;
using UnityPlugins.Input.Unity.Receivers;

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

		[Test]
		public void Trigger_WontTrigger_IfInputIsNotFresh()
		{
			InputReceiversManager manager = new InputReceiversManager();

			TestInputReceiver receiver = new TestInputReceiver();
			TestInputProvider provider = new TestInputProvider();
			provider.Result = new RawInputState(true);

			TriggerDetector detector = new TriggerDetector();
			detector.Add(provider);
			receiver.Updater.Add(detector);

			Assert.AreEqual(EInputStatus.Released, detector.Group.State.Status);
			manager.Update(0f);
			Assert.AreEqual(EInputStatus.Released, detector.Group.State.Status);

			//if key was pressed, while new receiver is pushed, all detectors should be "disabled" until input is released
			manager.PushReceiver(receiver, receiver.Updater, new InputLayerDefinition(string.Empty, 1, false));
			manager.Update(0f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(EInputStatus.Released, detector.Group.State.Status);
			
			//release key and update
			provider.Result = new RawInputState(false);
			manager.Update(0f);
			Assert.IsFalse(detector.IsActive);
			Assert.AreEqual(EInputStatus.Released, detector.Group.State.Status);

			//press key again
			provider.Result = new RawInputState(true);
			manager.Update(0f);
			Assert.True(detector.IsActive);
			//this is a bit counter intuitive, but makes sense
			//provider is added to detector, which has "AnyProvider" group-provider.
			//it updates status at the AnyProvider level, but update doesn't reach sub-providers
			//as it is not needed for the logic to work, and generally, specific providers shouldn't be checked like below
			Assert.AreEqual(EInputStatus.Released, provider.State.Status);
			Assert.AreEqual(EInputStatus.JustPressed, detector.Group.State.Status);
		}
	}
}

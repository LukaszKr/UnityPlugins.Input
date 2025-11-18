using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Providers
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class AInputProviderTests
	{
		[Test, Description("If provider wasn't updated in a frame, we assume it was used by something that was inactive, like popup. Reset the state.")]
		public void Update_SkipFrame_ResetState()
		{
			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			provider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.JustPressed), provider.State);
			provider.Update(3);
			Assert.AreEqual(new InputState(EInputStatus.Released), provider.State);
		}

		[Test, Description("Test that input provider goes through all the normal states if it remained active.")]
		public void Update_StatusLifecycle_Full()
		{
			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			provider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.JustPressed), provider.State);
			provider.Update(2);
			Assert.AreEqual(new InputState(EInputStatus.Pressed), provider.State);
			provider.Update(3);
			Assert.AreEqual(new InputState(EInputStatus.Pressed), provider.State);
			provider.Result = new RawInputState(false);
			provider.Update(4);
			Assert.AreEqual(new InputState(EInputStatus.JustReleased), provider.State);
			provider.Update(5);
			Assert.AreEqual(new InputState(EInputStatus.Released), provider.State);
			provider.Update(6);
			Assert.AreEqual(new InputState(EInputStatus.Released), provider.State);
		}

		[Test, Description("Case when provider was active for just 1 frame.")]
		public void Update_StatusLifecycle_DeactivateAfterFirstPress()
		{
			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			provider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.JustPressed), provider.State);
			provider.Result = new RawInputState(false);
			provider.Update(2);
			Assert.AreEqual(new InputState(EInputStatus.JustReleased), provider.State);
		}

		[Test, Description("Case when provider was activated on a frame it was deactivated.")]
		public void Update_StatusLifecycle_ActivateWhileReleasing()
		{
			TestInputProvider provider = new TestInputProvider(new RawInputState(true));
			provider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.JustPressed), provider.State);
			provider.Result = new RawInputState(false);
			provider.Update(2);
			Assert.AreEqual(new InputState(EInputStatus.JustReleased), provider.State);
			provider.Result = new RawInputState(true);
			provider.Update(3);
			Assert.AreEqual(new InputState(EInputStatus.JustPressed), provider.State);
		}
	}
}

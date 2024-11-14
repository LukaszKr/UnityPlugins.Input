using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Providers.Groups
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class AnyProviderTests : AGroupProviderTests<AnyProvider>
	{
		[Test, Description("If none of the sub providers is active, this one shouldn't be active either.")]
		public void MultipleProviders_NoneIsActive()
		{
			AnyProvider anyProvider = new AnyProvider();
			anyProvider.Add(new TestInputProvider());
			anyProvider.Add(new TestInputProvider());
			Assert.AreEqual(2, anyProvider.Count);

			anyProvider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.Released), anyProvider.State);
		}

		[Test, Description("If one detector is active, return it's state.")]
		public void MultipleProviders_OneIsActive()
		{
			RawInputState state = new RawInputState(1f);

			AnyProvider anyProvider = new AnyProvider();
			anyProvider.Add(new TestInputProvider());
			anyProvider.Add(new TestInputProvider(state));
			anyProvider.Add(new TestInputProvider());
			Assert.AreEqual(3, anyProvider.Count);

			anyProvider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.JustPressed, 1f), anyProvider.State);
			anyProvider.Update(2);
			Assert.AreEqual(new InputState(EInputStatus.Pressed, 1f), anyProvider.State);
		}

		[Test, Description("If multiple detectors are active, one with fake and one with real axis, real one takes priority.")]
		public void MultipleProviders_MultipleActive_ReturnRealAxis()
		{
			RawInputState smallAxis = new RawInputState(0.25f);
			RawInputState fakeAxis = new RawInputState(true);

			AnyProvider anyProvider = new AnyProvider();
			anyProvider.Add(new TestInputProvider(smallAxis));
			anyProvider.Add(new TestInputProvider(fakeAxis));
			Assert.AreEqual(2, anyProvider.Count);

			anyProvider.Update(1);
			InputState expected = new InputState(EInputStatus.JustPressed, 0.25f, true);
			Assert.AreEqual(expected, anyProvider.State);
		}

		[Test, Description("If multiple detectors with real axis are active, highest value will be returned.")]
		public void MultipleProviders_MultipleActive_ReturnHighestRealAxis()
		{
			RawInputState smallAxis = new RawInputState(0.25f);
			RawInputState largeAxis = new RawInputState(0.4f);

			AnyProvider anyProvider = new AnyProvider();
			anyProvider.Add(new TestInputProvider(smallAxis));
			anyProvider.Add(new TestInputProvider(largeAxis));
			Assert.AreEqual(2, anyProvider.Count);

			anyProvider.Update(1);
			InputState expected = new InputState(EInputStatus.JustPressed, 0.4f, true);
			Assert.AreEqual(expected, anyProvider.State);
		}
	}
}

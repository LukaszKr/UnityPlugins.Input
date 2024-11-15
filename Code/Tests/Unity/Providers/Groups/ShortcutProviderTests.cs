using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Providers.Groups
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class ShortcutProviderTests : AGroupProviderTests<ShortcutProvider>
	{
		[Test, Description("Shortcut requires all providers to be active.")]
		public void OnlyOneActiveProvider()
		{
			ShortcutProvider shortcutProvider = new ShortcutProvider();
			shortcutProvider.Add(new TestInputProvider());
			shortcutProvider.Add(new TestInputProvider(new RawInputState(true)));

			shortcutProvider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.Released, 0f, false), shortcutProvider.State);
		}

		[Test]
		public void AllProvidersActive_SameState()
		{
			RawInputState state = new RawInputState(true);
			ShortcutProvider shortcutProvider = new ShortcutProvider();
			shortcutProvider.Add(new TestInputProvider(state));
			shortcutProvider.Add(new TestInputProvider(state));

			shortcutProvider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.JustPressed), shortcutProvider.State);
		}

		[Test, Description("If shortcut consists of multiple real axis providers, highest value is returned.")]
		public void AllProvidersActive_DifferentRealAxis_HighestIsUsed()
		{
			RawInputState lowAxis = new RawInputState(0.2f);
			RawInputState highAxis = new RawInputState(0.4f);
			ShortcutProvider shortcutProvider = new ShortcutProvider();
			shortcutProvider.Add(new TestInputProvider(lowAxis));
			shortcutProvider.Add(new TestInputProvider(highAxis));

			shortcutProvider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.JustPressed, 0.4f), shortcutProvider.State);
		}
	}
}

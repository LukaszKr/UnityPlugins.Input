using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Providers.Groups
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public abstract class AGroupProviderTests<TProvider>
		where TProvider : AGroupProvider, new()
	{
		[Test, Description("Empty provider is valid, but never active.")]
		public void EmptyProvider()
		{
			TProvider groupProvider = new TProvider();
			Assert.AreEqual(0, groupProvider.Count);

			groupProvider.Update(1);
			Assert.AreEqual(new InputState(), groupProvider.State);
		}

		[Test, Description("With single inactive provider, group is not active.")]
		public void SingleProvider_Inactive_GroupIsInactive()
		{
			TProvider groupProvider = new TProvider();
			groupProvider.Add(new TestInputProvider());
			Assert.AreEqual(1, groupProvider.Count);

			groupProvider.Update(1);
			Assert.AreEqual(new InputState(), groupProvider.State);
		}

		[Test, Description("In case of single sub provider, mirror it's state.")]
		public void SingleProvider_Active()
		{
			RawInputState state = new RawInputState(1f);

			TProvider groupProvider = new TProvider();
			groupProvider.Add(new TestInputProvider(state));
			Assert.AreEqual(1, groupProvider.Count);

			groupProvider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.JustPressed, 1f, true), groupProvider.State);
		}

		[Test]
		public void MultipleProviders_AllInactive_GroupIsInactive()
		{
			TProvider groupProvider = new TProvider();
			groupProvider.Add(new TestInputProvider());
			groupProvider.Add(new TestInputProvider());
			Assert.AreEqual(2, groupProvider.Count);

			groupProvider.Update(1);
			Assert.AreEqual(new InputState(EInputStatus.Released, 0f, false), groupProvider.State);
		}
	}
}

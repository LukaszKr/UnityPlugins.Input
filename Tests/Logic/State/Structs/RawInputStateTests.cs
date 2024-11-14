using NUnit.Framework;

namespace UnityPlugins.Input.Unity.State.Structs
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class RawInputStateTests
	{
		[Test]
		public void Default()
		{
			Assert.AreEqual(new RawInputState(false, 0f, false), default(RawInputState));
		}
	}
}

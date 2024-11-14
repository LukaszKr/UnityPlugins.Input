using NUnit.Framework;

namespace UnityPlugins.Input.Unity.State.Structs
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class InputStateTests
	{
		[Test]
		public void Default()
		{
			Assert.AreEqual(new InputState(EInputStatus.Released, 0f, false), default(InputState));
		}
	}
}

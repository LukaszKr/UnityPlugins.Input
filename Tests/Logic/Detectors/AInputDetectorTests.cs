using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Detectors
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public abstract class AInputDetectorTests<TDetector>
		where TDetector : AInputDetector
	{
	}
}

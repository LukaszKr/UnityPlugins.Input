using UnityPlugins.Common.Tests;
using NUnit.Framework;

namespace UnityPlugins.Input.Unity.State.Enums
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class EInputStatusTests
	{
		public class GetNextTest : ATransformTest<EInputStatus, EInputStatus>
		{
			private bool m_IsActive;

			public GetNextTest(EInputStatus source, EInputStatus expected, bool isActive)
				: base(source, expected)
			{
				m_IsActive = isActive;
			}

			protected override EInputStatus Transform(EInputStatus status)
			{
				return status.GetNext(m_IsActive);
			}

			public override string ToString()
			{
				return $"{base.ToString()}, IsActive: {m_IsActive}";
			}
		}

		#region GetNext
		private static readonly GetNextTest[] m_GetNextTests = new GetNextTest[]
		{
			new GetNextTest(EInputStatus.Released, EInputStatus.Released, false),
			new GetNextTest(EInputStatus.JustReleased, EInputStatus.Released, false),
			new GetNextTest(EInputStatus.JustPressed, EInputStatus.JustReleased, false),
			new GetNextTest(EInputStatus.Pressed, EInputStatus.JustReleased, false),

			new GetNextTest(EInputStatus.Released, EInputStatus.JustPressed, true),
			new GetNextTest(EInputStatus.JustReleased, EInputStatus.JustPressed, true),
			new GetNextTest(EInputStatus.JustPressed, EInputStatus.Pressed, true),
			new GetNextTest(EInputStatus.Pressed, EInputStatus.Pressed, true),
		};

		[Test]
		public void GetNext([ValueSource(nameof(m_GetNextTests))] GetNextTest test)
		{
			test.Run();
		}
		#endregion
	}
}

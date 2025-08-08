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

		[Test, TestCaseSource(nameof(m_GetNextTests))]
		public void GetNext(GetNextTest test)
		{
			test.Run();
		}
		#endregion

		[Test]
		[TestCase(EInputStatus.JustPressed, true)]
		[TestCase(EInputStatus.Pressed, true)]
		[TestCase(EInputStatus.JustReleased, false)]
		[TestCase(EInputStatus.Released, false)]
		public void IsPressed(EInputStatus status, bool expected)
		{
			Assert.AreEqual(expected, status.IsPressed());
		}

		[Test]
		[TestCase(EInputStatus.JustPressed, false)]
		[TestCase(EInputStatus.Pressed, false)]
		[TestCase(EInputStatus.JustReleased, true)]
		[TestCase(EInputStatus.Released, true)]
		public void IsReleased(EInputStatus status, bool expected)
		{
			Assert.AreEqual(expected, status.IsReleased());
		}
	}
}
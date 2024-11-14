using System;

namespace UnityPlugins.Input.Unity.Providers
{
	public class TestInputProvider : AInputProvider
	{
		public RawInputState Result;

		public TestInputProvider(RawInputState result = default)
		{
			Result = result;
		}

		public override RawInputState GetRawState()
		{
			return Result;
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			throw new NotImplementedException();
		}

		protected override string ToStringImpl()
		{
			return $"{Result}";
		}
	}
}

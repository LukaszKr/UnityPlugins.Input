using System.Collections.Generic;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AListProvider: AInputProvider
	{
		public readonly List<AInputProvider> Providers = new List<AInputProvider>();

		public AListProvider()
			: base(EDeviceID.Unknown)
		{

		}

		protected override string ToStringImpl()
		{
			return $"[{Providers.JoinToString()}]";
		}
	}
}

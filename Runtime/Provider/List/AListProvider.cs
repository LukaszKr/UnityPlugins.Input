using System.Collections.Generic;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AListProvider: AInputProvider, IProviderContainer
	{
		protected readonly List<AInputProvider> m_Providers = new List<AInputProvider>();

		public AListProvider()
			: base(EDeviceID.Unknown)
		{

		}

		public void AddProvider(AInputProvider provider)
		{
			m_Providers.Add(provider);
		}

		protected override string ToStringImpl()
		{
			return $"[{m_Providers.JoinToString()}]";
		}
	}
}

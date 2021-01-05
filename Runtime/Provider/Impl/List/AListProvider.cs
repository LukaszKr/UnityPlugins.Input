using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AListProvider: AInputProvider, IProviderContainer
	{
		protected readonly List<AInputProvider> m_Providers = new List<AInputProvider>();

		public void AddProvider(AInputProvider provider)
		{
			m_Providers.Add(provider);
		}

		public void Sort()
		{
			m_Providers.SortProviders();
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			AListProvider otherProvider = (AListProvider)other;
			int compareResult;
			int thisCount = m_Providers.Count;
			int otherCount = otherProvider.m_Providers.Count;
			int count = Math.Min(thisCount, otherCount);
			for(int x = 0; x < count; ++x)
			{
				AInputProvider toCompareA = m_Providers[x];
				AInputProvider toCompareB = otherProvider.m_Providers[x];
				compareResult = toCompareA.CompareTo(toCompareB);
				if(compareResult != 0)
				{
					return compareResult;
				}
			}
			return thisCount.CompareTo(otherCount);
		}

		protected override string ToStringImpl()
		{
			return $"[{m_Providers.JoinToString()}]";
		}
	}
}

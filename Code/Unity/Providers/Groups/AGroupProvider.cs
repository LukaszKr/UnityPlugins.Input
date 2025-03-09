using System;
using System.Collections.Generic;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Input.Unity
{
	public abstract class AGroupProvider : AInputProvider
	{
		protected readonly List<AInputProvider> m_Providers = new List<AInputProvider>();

		public IReadOnlyList<AInputProvider> Providers => m_Providers;
		public int Count => m_Providers.Count;

		public AGroupProvider Add(AInputProvider provider)
		{
			m_Providers.Add(provider);
			return this;
		}

		public void Sort()
		{
			m_Providers.SortProviders();
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			AGroupProvider otherProvider = (AGroupProvider)other;
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

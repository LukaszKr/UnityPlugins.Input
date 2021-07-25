using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public abstract class AListProvider : AInputProvider, IProviderContainer
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

		public override bool Contains(AInputProvider provider)
		{
			AListProvider listProvider = provider as AListProvider;
			if(listProvider != null)
			{
				List<AInputProvider> toCheckList = listProvider.m_Providers;
				int toCheckCount = toCheckList.Count;
				for(int x = 0; x < toCheckCount; ++x)
				{
					AInputProvider toCheck = toCheckList[x];
					if(!ContainsProvider(toCheck))
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				return ContainsProvider(provider);
			}
		}

		private bool ContainsProvider(AInputProvider provider)
		{
			int count = m_Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider ownedProvider = m_Providers[x];
				if(ownedProvider.Contains(provider))
				{
					return true;
				}
			}
			return false;
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

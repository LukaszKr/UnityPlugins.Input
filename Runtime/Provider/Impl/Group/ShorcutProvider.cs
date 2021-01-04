using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class ShorcutProvider: AInputProvider, IProviderContainer
	{
		private readonly List<AInputProvider> m_Providers = new List<AInputProvider>();

		public ShorcutProvider()
		{
		}

		public void AddProvider(AInputProvider provider)
		{
			m_Providers.Add(provider);
		}

		public void Sort()
		{
			m_Providers.SortProviders();
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			float axis = 0f;
			bool isRealAxis = false;

			int count = m_Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_Providers[x];
				RawInputState data = provider.GetState(inputManager);
				if(!data.IsActive)
				{
					return new RawInputState(false);
				}
				//in case of key+axis combination, we want to return axis value
				if(data.IsRealAxis)
				{
					isRealAxis = true;
					axis = Math.Max(data.Axis, axis);
				}
				else if(!isRealAxis)
				{
					axis = data.Axis;
				}
			}

			return new RawInputState(true, axis, isRealAxis);
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			int compareResult;
			ShorcutProvider otherProvider = (ShorcutProvider)other;
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

using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class GroupProvider: AInputProvider, IProviderContainer
	{
		public EProviderListMode Mode;

		protected readonly List<AInputProvider> m_Providers = new List<AInputProvider>();

		public GroupProvider()
		{
		}

		public GroupProvider(EProviderListMode mode = EProviderListMode.Or)
		{
			Mode = mode;
		}

		public void AddProvider(AInputProvider provider)
		{
			m_Providers.Add(provider);
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			switch(Mode)
			{
				case EProviderListMode.And:
					return HandleAnd(inputManager);
				case EProviderListMode.Or:
					return HandleOr(inputManager);
				default:
					throw new NotImplementedException();
			}
		}

		private RawInputState HandleAnd(InputManager inputManager)
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
			}

			return new RawInputState(true, axis, isRealAxis);
		}

		private RawInputState HandleOr(InputManager inputManager)
		{
			float axis = 0f;
			bool isRealAxis = false;
			bool isAnyProviderActive = false;

			int count = m_Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_Providers[x];
				RawInputState data = provider.GetState(inputManager);
				if(data.IsActive)
				{
					isAnyProviderActive = true;

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
			}

			return new RawInputState(isAnyProviderActive, axis, isRealAxis);
		}

		protected override string ToStringImpl()
		{
			return $"[{m_Providers.JoinToString()}]";
		}
	}
}

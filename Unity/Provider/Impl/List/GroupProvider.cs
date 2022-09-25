using System;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public class GroupProvider : AListProvider
	{
		private AInputProvider m_UsedProvider;

		public AInputProvider UsedProvider => m_UsedProvider;

		public GroupProvider()
		{

		}

		protected override RawInputState GetState()
		{
			float axis = 0f;
			bool isRealAxis = false;
			bool isAnyProviderActive = false;
			m_UsedProvider = null;

			int count = m_Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_Providers[x];
				RawInputState data = provider.UpdateState(m_UpdateTick);
				if(data.IsActive)
				{
					isAnyProviderActive = true;
					m_UsedProvider = provider;
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
	}
}

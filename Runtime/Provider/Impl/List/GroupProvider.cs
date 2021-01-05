using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class GroupProvider: AListProvider
	{
		public GroupProvider()
		{

		}

		protected override RawInputState GetState(InputManager inputManager)
		{
			float axis = 0f;
			bool isRealAxis = false;
			bool isAnyProviderActive = false;

			int count = m_Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_Providers[x];
				RawInputState data = provider.UpdateState(inputManager);
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
	}
}

using System;

namespace UnityPlugins.Input.Unity
{
	public class AnyProvider : AGroupProvider
	{
		public override RawInputState GetRawState()
		{
			float axis = 0f;
			bool isRealAxis = false;
			bool isActive = false;

			int count = m_Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_Providers[x];
				RawInputState state = provider.GetRawState();
				if(state.IsActive)
				{
					isActive = true;
					//real axis takes priority
					if(state.IsRealAxis)
					{
						isRealAxis = true;
						axis = Math.Max(state.Axis, axis);
					}
					//if no real axis was found so far, get latest fake
					else if(!isRealAxis)
					{
						axis = state.Axis;
					}
				}
			}


			if(!isActive)
			{
				return new RawInputState(false);
			}

			return new RawInputState(axis, isRealAxis);
		}
	}
}

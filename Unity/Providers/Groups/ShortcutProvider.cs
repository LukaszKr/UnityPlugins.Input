using System;

namespace UnityPlugins.Input.Unity
{
	public class ShortcutProvider : AGroupProvider
	{
		public ShortcutProvider()
		{
		}

		public override RawInputState GetRawState()
		{
			float axis = 0f;
			bool isRealAxis = false;

			int activeCount = 0;

			int count = m_Providers.Count;

			if(count == 0)
			{
				return new RawInputState(false);
			}

			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_Providers[x];
				RawInputState state = provider.GetRawState();
				if(state.IsActive)
				{
					activeCount++;

					//in case of key+axis combination, we want to return axis value
					if(state.IsRealAxis)
					{
						isRealAxis = true;
						axis = Math.Max(state.Axis, axis);
					}
					else if(!isRealAxis)
					{
						axis = state.Axis;
					}
				}
			}

			if(activeCount != count)
			{
				return new RawInputState(false);
			}

			return new RawInputState(axis, isRealAxis);
		}
	}
}

using System;

namespace ProceduralLevel.Input.Unity
{
	public class ShortcutProvider : AListProvider
	{
		public ShortcutProvider()
		{
		}

		protected override InputState GetState()
		{
			float axis = 0f;
			bool isRealAxis = false;

			int count = m_Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_Providers[x];
				InputState data = provider.UpdateState(m_UpdateTick);
				if(!data.IsActive)
				{
					return new InputState(false);
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

			return new InputState(true, axis, isRealAxis);
		}
	}
}

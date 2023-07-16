using System;
using System.Globalization;

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

			int justPressedCount = 0;
			int pressedCount = 0;
			int justReleasedCount = 0;

			int count = m_Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = m_Providers[x];
				InputState data = provider.UpdateState(m_UpdateTick);
				switch(data.Status)
				{
					case EInputStatus.JustPressed:
						justPressedCount++;
						break;
					case EInputStatus.JustReleased:
						justReleasedCount++;
						break;
					case EInputStatus.Pressed:
						pressedCount++;
						break;
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

			EInputStatus status;
			if(pressedCount == count)
			{
				status = EInputStatus.Pressed;
			}
			else if(pressedCount+justPressedCount == count)
			{
				status = EInputStatus.JustPressed;
			}
			else if(pressedCount+justPressedCount+justReleasedCount == count)
			{
				status = EInputStatus.JustReleased;
			}
			else
			{
				return new InputState();
			}

			return new InputState(status, axis, isRealAxis);
		}
	}
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchDevice: APointerDevice
	{
		private const int TOUCH_COUNT = 5;

		public readonly TouchData[] Touches = new TouchData[TOUCH_COUNT];
		public int Count { get; private set; }

		public TouchDevice()
			: base(EDeviceID.Touch, TOUCH_COUNT)
		{
		}

		protected override bool IsPressed(int codeValue)
		{
			if(Count > 0)
			{
				return codeValue < Count;
			}
			return false;
		}

		public EButtonState Get(ETouchID touchID)
		{
			int index = (int)touchID;
			return m_KeyStates[index];
		}

		protected override void OnUpdateState(InputManager inputManager)
		{
			Touchscreen touchScreen = Touchscreen.current;
			if(touchScreen != null)
			{
				ReadOnlyArray<TouchControl> unityTouches = touchScreen.touches;
				int touchCount = Mathf.Min(unityTouches.Count, TOUCH_COUNT);
				int activeTouchOffset = 0;

				for(int x = 0; x < touchCount; ++x)
				{
					TouchControl touch = unityTouches[x];
					if(touch.isInProgress)
					{
						Touches[activeTouchOffset++] = new TouchData(touch.position.ReadValue(), touch.delta.ReadValue());
					}
				}
				Count = activeTouchOffset;
			}
			else
			{
				Count = 0;
			}
		}

		public override void GetActiveInputLinks(List<AInputLink> links)
		{
			if(IsActive)
			{
				int touchCount = Count;
				for(int x = 0; x < touchCount; ++x)
				{
					links.Add(new TouchInputLink((ETouchID)x));
				}
			}
		}
	}
}

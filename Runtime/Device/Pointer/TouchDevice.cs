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

		#region Getters
		public InputState Get(ETouchID touchID)
		{
			int index = (int)touchID;
			return m_InputState[index];
		}

		public EInputStatus GetStatus(ETouchID touchID)
		{
			int index = (int)touchID;
			return m_InputState[index].Status;
		}
		#endregion

		#region Update State

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

		public override void ResetState()
		{
			base.ResetState();

			Count = 0;
		}

		protected override RawInputState GetRawState(int rawInputID)
		{
			return new RawInputState(Count > 0 && rawInputID < Count);
		}
		#endregion

		public override void RecordProviders(List<AInputProvider> providers)
		{
			if(IsActive)
			{
				int touchCount = Count;
				for(int x = 0; x < touchCount; ++x)
				{
					providers.Add(new TouchProvider((ETouchID)x));
				}
			}
		}
	}
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchDevice : APointerDevice
	{
		public static readonly TouchDevice Instance = new TouchDevice();

		private const int TOUCH_COUNT = 5;

		public float DeltaSensitivityX = 200f;
		public float DeltaSensitivityY = 200f;

		public readonly TouchData[] Touches = new TouchData[TOUCH_COUNT];
		public int Count { get; private set; }

		public TouchDevice()
			: base(EDeviceID.Touch, TOUCH_COUNT)
		{
		}

		#region Getters
		public InputState Get(ETouchInputID touchID)
		{
			int index = (int)touchID;
			return m_InputState[index];
		}

		public EInputStatus GetStatus(ETouchInputID touchID)
		{
			int index = (int)touchID;
			return m_InputState[index].Status;
		}
		#endregion

		#region Update State

		protected override void OnUpdateState()
		{
			Touchscreen touchScreen = Touchscreen.current;
			if(touchScreen != null)
			{
				Rect screenRect = new Rect(0f, 0f, Screen.width,  Screen.height); //probably won't work with dual screens

				ReadOnlyArray<TouchControl> unityTouches = touchScreen.touches;
				int touchCount = Mathf.Min(unityTouches.Count, TOUCH_COUNT);
				int activeTouchOffset = 0;

				for(int x = 0; x < touchCount; ++x)
				{
					TouchControl touch = unityTouches[x];
					if(touch.isInProgress)
					{
						Vector2 position = touch.position.ReadValue();
						Vector2 screenDelta = touch.delta.ReadValue();
						Vector2 rawDelta = new Vector2(screenDelta.x/screenRect.width, screenDelta.y/screenRect.height);
						Vector2 delta = new Vector2(rawDelta.x*DeltaSensitivityX, rawDelta.y*DeltaSensitivityY);
						Touches[activeTouchOffset++] = new TouchData(position, screenDelta, rawDelta, delta);
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
					providers.Add(new TouchProvider((ETouchInputID)x));
				}
			}
		}
	}
}

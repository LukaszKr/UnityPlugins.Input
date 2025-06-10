using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace UnityPlugins.Input.Unity
{
	public class TouchDevice : APointerDevice
	{
		public static readonly TouchDevice Instance = new TouchDevice();

		private const int TOUCH_COUNT = 5;

		public float DeltaSensitivityX = 200f;
		public float DeltaSensitivityY = 200f;

		public readonly TouchData[] Touches = new TouchData[TOUCH_COUNT];
		public int Count { get; private set; }

		public override Vector2 ScreenDelta => Touches[1].ScreenDelta;
		public override Vector2 NormalizedDelta => Touches[1].RawDelta;
		public override Vector2 Delta => Touches[1].Delta;

		public override Vector2 Position => Touches[1].Position;
		public override Vector2 Scroll => default;
		public override bool ShowCursor => false;

		public TouchDevice()
			: base(TOUCH_COUNT)
		{
		}

		#region Getters
		public RawInputState Get(ETouchInputID touchID)
		{
			int index = (int)touchID;
			if(index >= 0 && index < m_InputState.Length)
			{
				return m_InputState[index];
			}
			return new RawInputState();
		}
		#endregion

		#region Update State

		protected override void OnUpdate()
		{
			base.OnUpdate();

			Touchscreen touchScreen = Touchscreen.current;
			if(touchScreen != null)
			{
				Rect screenRect = new Rect(0f, 0f, Screen.width,  Screen.height); //probably won't work with dual screens

				ReadOnlyArray<TouchControl> unityTouches = touchScreen.touches;
				int touchCount = Mathf.Min(unityTouches.Count, TOUCH_COUNT);
				int activeTouchOffset = 1;

				for(int x = 0; x < touchCount; ++x)
				{
					TouchControl touch = unityTouches[x];
					if(touch.isInProgress)
					{
						TouchData oldTouchData = Touches[activeTouchOffset];
						Vector2 position = touch.position.ReadValue();
						//touch.delta.ReadValue();
						Vector2 screenDelta = position-oldTouchData.Position;
						Vector2 rawDelta = new Vector2(screenDelta.x/screenRect.width, screenDelta.y/screenRect.height);
						Vector2 delta = new Vector2(rawDelta.x*DeltaSensitivityX, rawDelta.y*DeltaSensitivityY);
						Touches[activeTouchOffset++] = new TouchData(position, screenDelta, rawDelta, delta);
					}
				}
				Count = activeTouchOffset-1;
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

		protected override RawInputState GetState(int rawInputID)
		{
			if(rawInputID == 0) //None
			{
				return new RawInputState();
			}
			return new RawInputState(Count > 0 && rawInputID-1 < Count);
		}
		#endregion

		public override void GetActiveProviders(List<AInputProvider> providers)
		{
			if(IsActive)
			{
				int touchCount = Count;
				for(int x = 1; x <= touchCount; ++x)
				{
					providers.Add(new TouchProvider((ETouchInputID)x));
				}
			}
		}
	}
}

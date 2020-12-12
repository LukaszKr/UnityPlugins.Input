using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseDevice: AInputDevice
	{
		public static float AxisDeadZone = 0.19f;
		public static float ButtonDeadZone = 0.5f;
		private const int INPUT_COUNT = EMouseButtonExt.MAX_VALUE;

		public Vector2 PositionDelta { get; private set; }
		public Vector2 Position { get; private set; }
		public Vector2 Scroll { get; private set; }

		private Mouse m_Mouse;

		public MouseDevice()
			: base(EDeviceID.Mouse, INPUT_COUNT)
		{
		}

		#region Getters
		public InputState Get(EMouseButton button)
		{
			return m_InputState[(int)button];
		}

		public EInputStatus GetStatus(EMouseButton button)
		{
			return m_InputState[(int)button].Status;
		}

		public float GetAxis(EMouseButton button)
		{
			return m_InputState[(int)button].Axis;
		}
		#endregion

		#region Update State
		protected override void OnUpdateState(InputManager inputManager)
		{
			m_Mouse = Mouse.current;
			Scroll = (m_Mouse != null ? m_Mouse.scroll.ReadValue() : new Vector2(0f, 0f));

			Vector2 oldPosition = Position;

			Position = (m_Mouse != null ? m_Mouse.position.ReadValue() : new Vector2(0f, 0f));
			Rect screenRect = new Rect(0f, 0f, Screen.width,  Screen.height); //probably won't work with dual screens
			if(screenRect.Contains(oldPosition)) //prevent massive delta changes when out of focus 
			{
				PositionDelta = Position-oldPosition;
			}
			else
			{
				PositionDelta = Vector2.zero;
			}

			m_IsActive |= PositionDelta.sqrMagnitude > 0.1f;
		}

		protected override RawInputState GetRawState(int inputID)
		{
			if(m_Mouse == null)
			{
				return new RawInputState(false);
			}
			EMouseButton button = (EMouseButton)inputID;
			switch(button)
			{
				case EMouseButton.Left:
					return new RawInputState(m_Mouse.leftButton.isPressed);
				case EMouseButton.Right:
					return new RawInputState(m_Mouse.rightButton.isPressed);
				case EMouseButton.Middle:
					return new RawInputState(m_Mouse.middleButton.isPressed);
				case EMouseButton.Back:
					return new RawInputState(m_Mouse.backButton.isPressed);
				case EMouseButton.Forward:
					return new RawInputState(m_Mouse.forwardButton.isPressed);
				default:
					if(button.IsAxis())
					{
						float scrollValue = ReadScrollValue(button);
						return new RawInputState(scrollValue >= AxisDeadZone, scrollValue);
					}
					throw new NotImplementedException();
			}
		}

		private float ReadScrollValue(EMouseButton button)
		{
			switch(button)
			{
				case EMouseButton.ScrollForward:
					return Math.Max(0f, Scroll.y);
				case EMouseButton.ScrollBackward:
					return -Math.Min(0f, Scroll.y);
				case EMouseButton.ScrollRight:
					return Math.Max(0f, Scroll.x);
				case EMouseButton.ScrollLeft:
					return -Math.Min(0f, Scroll.x);
				default:
					throw new NotImplementedException();
			}
		}

		protected override void OnSkippedFrame()
		{
			Position = m_Mouse.position.ReadValue();
		}
		#endregion

		public override void RecordProviders(List<AInputProvider> providers)
		{
			if(IsActive)
			{
				int length = m_InputState.Length;
				for(int x = 0; x < length; ++x)
				{
					InputState state = m_InputState[x];
					if(state.IsActive)
					{
						providers.Add(new MouseProvider((EMouseButton)x));
					}
				}
			}
		}
	}
}

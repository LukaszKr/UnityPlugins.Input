using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseDevice: AInputDevice
	{
		public float MoveAxisDeadZone = 0.02f;
		public float DeltaSensitivityX = 350f;
		public float DeltaSensitivityY = 350f;
		public float ScrollSensitivity = 5f;

		public Vector2 PositionDelta { get; private set; }
		public Vector2 Delta { get; private set; }
		public Vector2 Position { get; private set; }
		public Vector2 Scroll { get; private set; }

		private Mouse m_Mouse;

		public MouseDevice()
			: base(EDeviceID.Mouse, EMouseInputIDExt.Meta.MaxValue+1)
		{
		}

		#region Getters
		public InputState Get(EMouseInputID inputID)
		{
			return m_InputState[(int)inputID];
		}

		public EInputStatus GetStatus(EMouseInputID inputID)
		{
			return m_InputState[(int)inputID].Status;
		}

		public float GetAxis(EMouseInputID button)
		{
			return m_InputState[(int)button].Axis;
		}
		#endregion

		#region Update State
		protected override void OnUpdateState(InputManager inputManager)
		{
			m_Mouse = Mouse.current;
			if(m_Mouse != null)
			{
				Vector2 rawScroll = m_Mouse.scroll.ReadValue();
				float scrollX = NormalizeScroll(rawScroll.x)*ScrollSensitivity;
				float scrollY = NormalizeScroll(rawScroll.y)*ScrollSensitivity;
				Scroll = new Vector2(scrollX, scrollY);
			}
			else
			{
				Scroll = new Vector2(0f, 0f);
			}

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

			float deltaX = (PositionDelta.x/screenRect.width)*DeltaSensitivityX;
			float deltaY = (PositionDelta.y/screenRect.height)*DeltaSensitivityY;
			Delta = new Vector2(deltaX, deltaY);

			m_IsActive |= PositionDelta.sqrMagnitude > 0.1f;
		}

		protected override RawInputState GetRawState(int rawInputID)
		{
			if(m_Mouse == null)
			{
				return new RawInputState(false);
			}
			EMouseInputID inputID = (EMouseInputID)rawInputID;
			switch(inputID)
			{
				case EMouseInputID.Left:
					return new RawInputState(m_Mouse.leftButton.isPressed);
				case EMouseInputID.Right:
					return new RawInputState(m_Mouse.rightButton.isPressed);
				case EMouseInputID.Middle:
					return new RawInputState(m_Mouse.middleButton.isPressed);
				case EMouseInputID.Back:
					return new RawInputState(m_Mouse.backButton.isPressed);
				case EMouseInputID.Forward:
					return new RawInputState(m_Mouse.forwardButton.isPressed);
				default:
					if(inputID.IsScroll())
					{
						float scrollValue = ReadScrollValue(inputID);
						return new RawInputState(scrollValue >= 0.01f, scrollValue);
					}
					else if(inputID.IsMove())
					{
						float moveValue = ReadMoveValue(inputID);
						return new RawInputState(moveValue >= MoveAxisDeadZone, moveValue);
					}
					throw new NotImplementedException();
			}
		}

		private float ReadScrollValue(EMouseInputID inputID)
		{
			switch(inputID)
			{
				case EMouseInputID.ScrollForward:
					return Math.Max(0f, Scroll.y);
				case EMouseInputID.ScrollBackward:
					return -Math.Min(0f, Scroll.y);
				case EMouseInputID.ScrollRight:
					return Math.Max(0f, Scroll.x);
				case EMouseInputID.ScrollLeft:
					return -Math.Min(0f, Scroll.x);
				default:
					throw new NotImplementedException();
			}
		}

		private float ReadMoveValue(EMouseInputID inputID)
		{
			switch(inputID)
			{
				case EMouseInputID.MoveLeft:
					return -PositionDelta.x;
				case EMouseInputID.MoveRight:
					return PositionDelta.x;
				case EMouseInputID.MoveUp:
					return PositionDelta.y;
				case EMouseInputID.MoveDown:
					return -PositionDelta.y;
				default:
					throw new NotImplementedException();
			}
		}

		private float NormalizeScroll(float value)
		{
			if(value < -0.01f)
			{
				return -1f;
			}
			else if(value > 0.01f)
			{
				return 1f;
			}
			return 0f;
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
						providers.Add(new MouseProvider((EMouseInputID)x));
					}
				}
			}
		}
	}
}

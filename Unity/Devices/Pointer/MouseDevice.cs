using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityPlugins.Input.Unity
{
	public class MouseDevice : APointerDevice
	{
		public static readonly MouseDevice Instance = new MouseDevice();

		public float MoveAxisDeadZone = 0.000001f;
		public float DeltaSensitivityX = 200f;
		public float DeltaSensitivityY = 200f;
		public float ScrollSensitivity = 5f;
		public float MoveAxisMultiplierX = 1f;
		public float MoveAxisMultiplierY = 1f;

		private Mouse m_Mouse;

		private Vector2 m_ScreenDelta;
		private Vector2 m_NormalizedDelta;
		private Vector2 m_Delta;

		private Vector2 m_Position;
		private Vector2 m_Scroll;

		public override Vector2 ScreenDelta => m_ScreenDelta;
		public override Vector2 NormalizedDelta => m_NormalizedDelta;
		public override Vector2 Delta => m_Delta;

		public override Vector2 Position => m_Position;
		public override Vector2 Scroll => m_Scroll;
		public override bool ShowCursor => true;

		public MouseDevice()
			: base(EMouseInputIDExt.Meta.MaxValue+1)
		{
		}

		#region Getters
		public RawInputState Get(EMouseInputID inputID)
		{
			int index = (int)inputID;
			if(inputID >= 0 && index < m_InputState.Length)
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

			m_Mouse = Mouse.current;
			Vector2 resolution = new Vector2(Screen.width,  Screen.height); //probably won't work with dual screens

			if(m_Mouse != null)
			{
				Vector2 rawScroll = m_Mouse.scroll.ReadValue();
				float scrollX = NormalizeScroll(rawScroll.x)*ScrollSensitivity;
				float scrollY = NormalizeScroll(rawScroll.y)*ScrollSensitivity;
				m_Scroll = new Vector2(scrollX, scrollY);
				Vector2 oldPosition = m_Position;
				m_Position = m_Mouse.position.ReadValue();
				//can't use mouse.delta here due to unity not applying screen scale to delta, but it applies it to position
				//let's say you force 1920x1080 resolultion on game window, but its' not taking whole screen so it's at 0.5 scale
				//lower right corner mouse position will be 1920x1080, but delta movement will be only 960x540 pixels.
				//delta is real screen pixels, while mouse position is window actual.
				//so using mouse.delta would lead to different behaviour of drag sensitivity
				m_ScreenDelta = m_Position-oldPosition;
			}
			else
			{
				m_Scroll = default;
				m_Position = default;
				m_ScreenDelta = default;
			}

			float deltaX = (ScreenDelta.x/resolution.x);
			float deltaY = (ScreenDelta.y/resolution.y);
			m_NormalizedDelta = new Vector2(deltaX, deltaY);
			m_Delta = new Vector2(deltaX*DeltaSensitivityX, deltaY*DeltaSensitivityY);

			m_IsActive |= ScreenDelta.sqrMagnitude > 0.1f;
		}

		protected override RawInputState GetState(int rawInputID)
		{
			if(m_Mouse == null)
			{
				return new RawInputState(false);
			}
			EMouseInputID inputID = (EMouseInputID)rawInputID;
			switch(inputID)
			{
				case EMouseInputID.None:
					return new RawInputState();
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
					return -m_NormalizedDelta.x*MoveAxisMultiplierX;
				case EMouseInputID.MoveRight:
					return m_NormalizedDelta.x*MoveAxisMultiplierX;
				case EMouseInputID.MoveUp:
					return m_NormalizedDelta.y*MoveAxisMultiplierY;
				case EMouseInputID.MoveDown:
					return -m_NormalizedDelta.y*MoveAxisMultiplierY;
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
			if(m_Mouse != null)
			{
				m_Position = m_Mouse.position.ReadValue();
			}
		}
		#endregion

		public override void GetActiveProviders(List<AInputProvider> providers)
		{
			if(IsActive)
			{
				int length = m_InputState.Length;
				for(int x = 0; x < length; ++x)
				{
					RawInputState state = m_InputState[x];
					if(state.IsActive)
					{
						providers.Add(new MouseProvider((EMouseInputID)x));
					}
				}
			}
		}
	}
}

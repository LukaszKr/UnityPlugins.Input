﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public class MouseDevice : APointerDevice
	{
		public static readonly MouseDevice Instance = new MouseDevice();

		public float MoveAxisDeadZone = 0.02f;
		public float DeltaSensitivityX = 200f;
		public float DeltaSensitivityY = 200f;
		public float ScrollSensitivity = 5f;

		private Mouse m_Mouse;

		private Vector2 m_ScreenDelta;
		private Vector2 m_RawDelta;
		private Vector2 m_Delta;

		private Vector2 m_Position;
		private Vector2 m_Scroll;

		public override Vector2 ScreenDelta => m_ScreenDelta;
		public override Vector2 RawDelta => m_RawDelta;
		public override Vector2 Delta => m_Delta;

		public override Vector2 Position => m_Position;
		public override Vector2 Scroll => m_Scroll;

		public MouseDevice()
			: base(EDeviceID.Mouse, EMouseInputIDExt.Meta.MaxValue+1)
		{
		}

		#region Getters
		public InputState Get(EMouseInputID inputID)
		{
			return GetRawState((int)inputID);
		}

		public float GetAxis(EMouseInputID inputID)
		{
			return GetRawState((int)inputID).Axis;
		}
		#endregion

		#region Update State
		protected override void OnUpdateState()
		{
			m_Mouse = Mouse.current;
			if(m_Mouse != null)
			{
				Vector2 rawScroll = m_Mouse.scroll.ReadValue();
				float scrollX = NormalizeScroll(rawScroll.x)*ScrollSensitivity;
				float scrollY = NormalizeScroll(rawScroll.y)*ScrollSensitivity;
				m_Scroll = new Vector2(scrollX, scrollY);
				m_Position = m_Mouse.position.ReadValue();
				m_ScreenDelta = m_Mouse.delta.ReadValue();
			}
			else
			{
				m_Scroll = default;
				m_Position = default;
				m_ScreenDelta = default;
			}

			Rect screenRect = new Rect(0f, 0f, Screen.width,  Screen.height); //probably won't work with dual screens
			float deltaX = (ScreenDelta.x/screenRect.width);
			float deltaY = (ScreenDelta.y/screenRect.height);
			m_RawDelta = new Vector2(deltaX, deltaY);
			m_Delta = new Vector2(deltaX*DeltaSensitivityX, deltaY*DeltaSensitivityY);

			m_IsActive |= ScreenDelta.sqrMagnitude > 0.1f;
		}

		protected override InputState GetRawState(int rawInputID)
		{
			if(m_Mouse == null)
			{
				return new InputState(false);
			}
			EMouseInputID inputID = (EMouseInputID)rawInputID;
			switch(inputID)
			{
				case EMouseInputID.Left:
					return new InputState(m_Mouse.leftButton.isPressed);
				case EMouseInputID.Right:
					return new InputState(m_Mouse.rightButton.isPressed);
				case EMouseInputID.Middle:
					return new InputState(m_Mouse.middleButton.isPressed);
				case EMouseInputID.Back:
					return new InputState(m_Mouse.backButton.isPressed);
				case EMouseInputID.Forward:
					return new InputState(m_Mouse.forwardButton.isPressed);
				default:
					if(inputID.IsScroll())
					{
						float scrollValue = ReadScrollValue(inputID);
						return new InputState(scrollValue >= 0.01f, scrollValue);
					}
					else if(inputID.IsMove())
					{
						float moveValue = ReadMoveValue(inputID);
						return new InputState(moveValue >= MoveAxisDeadZone, moveValue);
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
					return -Delta.x;
				case EMouseInputID.MoveRight:
					return Delta.x;
				case EMouseInputID.MoveUp:
					return Delta.y;
				case EMouseInputID.MoveDown:
					return -Delta.y;
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
			m_Position = m_Mouse.position.ReadValue();
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

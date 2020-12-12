using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseDevice : AInputDevice
	{
		public static float AxisDeadZone = 0.19f;
		public static float ButtonDeadZone = 0.5f;
		private const int STATE_SIZE = EMouseButtonExt.MAX_VALUE;

		private bool m_AnyScrollActive;
		public bool AnyScrollActive { get { return m_AnyScrollActive; } }

		public Vector2 PositionDelta { get; private set; }
		public Vector2 Position { get; private set; }
		public Vector2 Scroll { get; private set; }

		private Mouse m_Mouse;
		private readonly float[] m_AxesStates;

		public MouseDevice()
			: base(EDeviceID.Mouse, STATE_SIZE)
		{
			m_AxesStates = new float[EMouseButtonExt.MAX_VALUE+1];

		}

		protected override void OnSkippedFrame()
		{
			Position = m_Mouse.position.ReadValue();
		}

		protected override void OnUpdateState(InputManager inputManager)
		{
			m_AnyScrollActive = false;
			m_Mouse = Mouse.current;

			Vector2 oldPosition = Position;

			Position = (m_Mouse != null? m_Mouse.position.ReadValue(): new Vector2(0f, 0f));
			Rect screenRect = new Rect(0f, 0f, Screen.width,  Screen.height); //probably won't work with dual screens
			if(screenRect.Contains(oldPosition)) //prevent massive delta changes when out of focus 
			{
				PositionDelta = Position-oldPosition;
			}
			else
			{
				PositionDelta = Vector2.zero;
			}
			ProcessScroll();


			m_IsActive = m_IsActive || m_AnyScrollActive || PositionDelta.sqrMagnitude > 0.1f;
		}

		private void ProcessScroll()
		{
			Scroll = (m_Mouse != null ? m_Mouse.scroll.ReadValue() : new Vector2(0f, 0f));

			int axesCount = m_AxesStates.Length;
			for(int x = 0; x < axesCount; ++x)
			{
				m_AxesStates[x] = 0f;
			}

			if(Scroll.x > 0)
			{
				ProcessAxis(EMouseButton.ScrollRight, Scroll.x);
			}
			else 
			{
				ProcessAxis(EMouseButton.ScrollLeft, -Scroll.x);
			}
			if(Scroll.y > 0)
			{
				ProcessAxis(EMouseButton.ScrollForward, Scroll.y);
			}
			else
			{
				ProcessAxis(EMouseButton.ScrollBackward, -Scroll.y);
			}
		}

		private void ProcessAxis(EMouseButton axis, float value)
		{
			m_AxesStates[(int)axis] = value;
			if(value >= AxisDeadZone)
			{
				m_AnyScrollActive = true;
			}
		}

		protected override bool IsPressed(int codeValue) 
		{
			if(m_Mouse == null)
			{
				return false;
			}
			EMouseButton button = (EMouseButton)codeValue;
			switch(button)
			{
				case EMouseButton.Left:
					return m_Mouse.leftButton.isPressed;
				case EMouseButton.Right:
					return m_Mouse.rightButton.isPressed;
				case EMouseButton.Middle:
					return m_Mouse.middleButton.isPressed;
				case EMouseButton.Back:
					return m_Mouse.backButton.isPressed;
				case EMouseButton.Forward:
					return m_Mouse.forwardButton.isPressed;
				default:
					return GetAxis(button) >= ButtonDeadZone;
			}
		}

		public EButtonState Get(EMouseButton button)
		{
			return m_KeyStates[(int)button];
		}

		public float GetAxis(EMouseButton button)
		{
			if(button.IsAxis())
			{
				return m_AxesStates[(int)button];
			}
			else
			{
				return (EButtonState.IsDown.Contains(Get(button))? 1f: 0f);
			}
		}

		public override void GetActiveInputLinks(List<AInputLink> links)
		{
			if(IsActive)
			{
				int length = m_KeyStates.Length;
				for(int x = 0; x < length; ++x)
				{
					EButtonState state = m_KeyStates[x];
					if(EButtonState.IsDown.Contains(state))
					{
						links.Add(new MouseInputLink((EMouseButton)x));
					}
				}
			}
		}
	}
}

using UnityEngine;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class MouseDevice : AInputDevice
	{
		private const int STATE_SIZE = 5;

		public Vector2 PositionDelta { get; private set; }
		public Vector2 Position { get; private set; }
		public Vector2 Scroll { get; private set; }

		private Mouse m_Mouse;

		public MouseDevice()
			: base(DeviceID.Mouse, STATE_SIZE)
		{
		}

		protected override void OnSkippedFrame()
		{
			Position = m_Mouse.position.ReadValue();
		}

		protected override void OnUpdateState(InputManager inputManager)
		{
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
			Scroll = (m_Mouse != null? m_Mouse.scroll.ReadValue(): new Vector2(0f, 0f));

			m_IsActive = m_IsActive || Scroll.sqrMagnitude > 0.01f || PositionDelta.sqrMagnitude > 0.1f;
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
			}
			return false;
		}

		public EButtonState Get(EMouseButton button)
		{
			return m_KeyStates[(int)button];
		}
	}
}

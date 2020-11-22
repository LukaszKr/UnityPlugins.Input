using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class KeyboardDevice: AInputDevice
	{
		private const int STATE_SIZE = 111;
		private Keyboard m_Keyboard = null;

		static KeyboardDevice()
		{
		}

		public KeyboardDevice()
			: base(DeviceID.Keyboard, STATE_SIZE)
		{
		}

		protected override void OnUpdateState(InputManager inputManager)
		{
			m_Keyboard = Keyboard.current;
		}

		protected override bool IsPressed(int codeValue)
		{
			if(codeValue > 0 && m_Keyboard != null)
			{
				Key key = (Key)codeValue;
				return m_Keyboard[key].isPressed;
			}
			return false;
		}

		public EButtonState Get(Key key)
		{
			return m_KeyStates[(int)key];
		}
	}
}

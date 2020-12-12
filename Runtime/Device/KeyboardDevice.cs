using System.Collections.Generic;
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
			: base(EDeviceID.Keyboard, STATE_SIZE)
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
						links.Add(new KeyboardInputLink((Key)x));
					}
				}
			}
		}
	}
}

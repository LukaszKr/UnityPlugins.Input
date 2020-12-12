using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class KeyboardDevice: AInputDevice
	{
		private const int INPUT_COUNT = 111;
		private Keyboard m_Keyboard = null;

		static KeyboardDevice()
		{
		}

		public KeyboardDevice()
			: base(EDeviceID.Keyboard, INPUT_COUNT)
		{
		}

		#region Getters
		public InputState Get(Key key)
		{
			return m_InputState[(int)key];
		}

		public EInputStatus GetStatus(Key key)
		{
			return m_InputState[(int)key].Status;
		}
		#endregion

		#region UpdateState
		protected override void OnUpdateState(InputManager inputManager)
		{
			m_Keyboard = Keyboard.current;
		}

		protected override RawInputState GetRawState(int inputID)
		{
			if(inputID > 0 && m_Keyboard != null)
			{
				Key key = (Key)inputID;
				return new RawInputState(m_Keyboard[key].isPressed);
			}
			return new RawInputState(false);
		}
		#endregion

		public override void GetActiveInputLinks(List<AInputLink> links)
		{
			if(IsActive)
			{
				int length = m_InputState.Length;
				for(int x = 0; x < length; ++x)
				{
					InputState state = m_InputState[x];
					if(state.IsActive)
					{
						links.Add(new KeyboardInputLink((Key)x));
					}
				}
			}
		}
	}
}

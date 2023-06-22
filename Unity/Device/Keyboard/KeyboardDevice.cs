using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public class KeyboardDevice : AInputDevice
	{
		public static readonly KeyboardDevice Instance = new KeyboardDevice();

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
		public InputState Get(Key inputID)
		{
			return m_InputState[(int)inputID];
		}
		#endregion

		#region UpdateState
		protected override void OnUpdateState()
		{
			m_Keyboard = Keyboard.current;
		}

		protected override InputState GetRawState(int rawInputID)
		{
			if(rawInputID > 0 && m_Keyboard != null)
			{
				Key key = (Key)rawInputID;
				return new InputState(m_Keyboard[key].isPressed);
			}
			return new InputState(false);
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
						providers.Add(new KeyboardProvider((Key)x));
					}
				}
			}
		}
	}
}

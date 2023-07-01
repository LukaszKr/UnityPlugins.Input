using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace ProceduralLevel.Input.Unity
{
	public class KeyboardDevice : ABaseInputDevice
	{
		public static readonly KeyboardDevice Instance = new KeyboardDevice();

		private bool m_IsActive;
		private Keyboard m_Keyboard = null;

		public override bool IsActive => m_IsActive;
		public override bool IsAnyKeyActive => m_IsActive;

		static KeyboardDevice()
		{
		}

		public KeyboardDevice()
			: base(EDeviceID.Keyboard)
		{
		}

		#region Getters
		public InputState Get(Key inputID)
		{
			if(m_Keyboard != null)
			{
				return new InputState(m_Keyboard[inputID].isPressed);
			}
			return new InputState(false);
		}
		#endregion

		#region UpdateState
		protected override void OnUpdateState()
		{
			m_Keyboard = Keyboard.current;
			m_IsActive = (m_Keyboard != null && m_Keyboard.anyKey.isPressed);
		}

		public override void ResetState()
		{
			m_IsActive = false;
		}
		#endregion

		public override void GetActiveProviders(List<AInputProvider> providers)
		{
			if(IsActive)
			{
				ReadOnlyArray<KeyControl> keys = m_Keyboard.allKeys;
				for(int x = 1; x < keys.Count; ++x)
				{
					KeyControl key = keys[x];
					if(key.isPressed)
					{
						providers.Add(new KeyboardProvider(key.keyCode));
					}
				}
			}
		}
	}
}

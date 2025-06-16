using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace UnityPlugins.Input.Unity
{
	public class KeyboardDevice : AInputDevice
	{
		public static readonly KeyboardDevice Instance = new KeyboardDevice();

		private bool m_IsActive;
		private Keyboard m_Keyboard = null;

		public override bool IsActive => m_IsActive;
		public override bool IsAnyKeyActive => m_IsActive;
		public override bool ShowCursor => true;

		static KeyboardDevice()
		{
		}

		#region Getters
		public RawInputState Get(Key inputID)
		{
			if(inputID == Key.None)
			{
				return new RawInputState();
			}
			if(m_Keyboard != null)
			{
				if(inputID > Key.None && (int)inputID <= Keyboard.KeyCount)
				{
					return m_Keyboard[inputID].ToRawInputState();
				}
			}
			return new RawInputState();
		}
		#endregion

		#region UpdateState
		protected override void OnUpdate()
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

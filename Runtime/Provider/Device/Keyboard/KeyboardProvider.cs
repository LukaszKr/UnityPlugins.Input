﻿using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class KeyboardProvider: AButtonProvider
	{
		public Key Key;

		public KeyboardProvider(Key key)
		{
			Key = key;
		}

		protected override RawInputState GetInputStatus(InputManager inputManager)
		{
			return inputManager.Keyboard.Get(Key).ToRaw();
		}


		public override string ToString()
		{
			return string.Format("[Key: {0}]", Key);
		}
	}
}
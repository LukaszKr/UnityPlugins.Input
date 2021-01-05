﻿using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class KeyboardProvider: AInputProvider
	{
		public Key InputID;

		public KeyboardProvider()
		{
		}

		public KeyboardProvider(Key inputID)
		{
			InputID = inputID;
		}

		protected override RawInputState GetState(InputManager inputManager)
		{
			return inputManager.Keyboard.Get(InputID).ToRaw();
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			KeyboardProvider otherProvider = (KeyboardProvider)other;
			return InputID.CompareTo(otherProvider.InputID);
		}

		protected override string ToStringImpl()
		{
			return $"{InputID}";
		}
	}
}

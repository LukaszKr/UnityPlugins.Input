﻿namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AButtonProvider: AInputProvider
	{
		private bool m_WasPressed = false;
		private int m_UpdateTick = 0;

		protected override RawInputState OnRefresh(InputManager inputManager)
		{
			int newUpdateTick = inputManager.UpdateTick;
			if(newUpdateTick-1 != m_UpdateTick)
			{
				m_WasPressed = false;
			}

			m_UpdateTick = newUpdateTick;

			EInputStatus buttonState = GetButtonState(inputManager);
			if(buttonState == EInputStatus.JustPressed)
			{
				m_WasPressed = true;
				return new RawInputState(true);
			}
			else if(buttonState == EInputStatus.JustReleased)
			{
				m_WasPressed = false;
			}
			else if(m_WasPressed && buttonState == EInputStatus.Pressed)
			{
				return new RawInputState(true);
			}
			return new RawInputState(false);
		}

		protected abstract EInputStatus GetButtonState(InputManager inputManager);
	}
}

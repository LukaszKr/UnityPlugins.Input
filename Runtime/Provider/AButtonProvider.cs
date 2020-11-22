namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AButtonProvider: AInputProvider
	{
		private bool m_WasPressed = false;
		private int m_UpdateTick = 0;

		protected override InputProviderData OnRefresh(InputManager inputManager)
		{
			int newUpdateTick = inputManager.UpdateTick;
			if(newUpdateTick-1 != m_UpdateTick)
			{
				m_WasPressed = false;
			}

			m_UpdateTick = newUpdateTick;

			EButtonState buttonState = GetButtonState(inputManager);
			if(buttonState == EButtonState.JustPressed)
			{
				m_WasPressed = true;
				return new InputProviderData(true);
			}
			else if(buttonState == EButtonState.JustReleased)
			{
				m_WasPressed = false;
			}
			else if(m_WasPressed && buttonState == EButtonState.Pressed)
			{
				return new InputProviderData(true);
			}
			return new InputProviderData(false);
		}

		protected abstract EButtonState GetButtonState(InputManager inputManager);
	}
}

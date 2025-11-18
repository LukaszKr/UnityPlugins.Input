namespace UnityPlugins.Input.Unity
{
	public class TriggerDetector : AInputDetector
	{
		private bool m_Fired = false;

		protected override bool OnInputUpdate(InputState inputState, float deltaTime)
		{
			if(m_Fired)
			{
				return false;
			}

			m_Fired = true;

			if(inputState.Status != EInputStatus.JustPressed)
			{
				return false;
			}

			return true;
		}

		protected override void OnResetState()
		{
			m_Fired = false;
		}
	}
}

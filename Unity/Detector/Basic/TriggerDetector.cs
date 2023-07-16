namespace ProceduralLevel.Input.Unity
{
	public class TriggerDetector : AInputDetector
	{
		private bool m_Fired = false;

		protected override bool OnInputUpdate(InputState inputState, float deltaTime)
		{
			if(inputState.Status == EInputStatus.Pressed)
			{
				return false;
			}
			if(!m_Fired)
			{
				m_Fired = true;
				return true;
			}
			return false;
		}

		protected override void OnInputReset()
		{
			m_Fired = false;
		}
	}
}

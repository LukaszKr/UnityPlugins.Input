﻿namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public class TriggerDetector : AInputDetector
	{
		private bool m_Fired = false;

		protected override bool OnInputUpdate(float deltaTime)
		{
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

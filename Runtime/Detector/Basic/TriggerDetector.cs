﻿namespace ProceduralLevel.UnityPlugins.Input
{
	public class TriggerDetector: AInputDetector
	{
		private bool m_Fired = false;

		protected override bool OnInputUpdate(InputManager inputManager)
		{
			if(!m_Fired)
			{
				m_Fired = true;
				return true;
			}
			return false;
		}

		protected override void OnInputReset(InputManager inputManager)
		{
			m_Fired = false;
		}
	}
}

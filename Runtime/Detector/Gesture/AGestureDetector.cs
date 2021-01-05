namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AGestureDetector: ADetector
	{
		private bool m_Started;
		private bool m_IsUpdating;

		public override bool Triggered { get { return m_Started && m_IsUpdating; } }

		protected override void OnUpdate(InputManager inputManager)
		{
			TouchDevice touch = inputManager.Touch;
			if(IsGestureActive(touch))
			{
				if(m_Started)
				{
					m_IsUpdating = UpdateGesture(touch);
				}
				else
				{
					StartGesture(touch);
					m_Started = true;
				}
			}
			else if(m_Started)
			{
				EndGesture(touch);
				m_Started = false;
			}
		}

		public override void Validate(InputValidator resolver)
		{
		}

		protected abstract bool IsGestureActive(TouchDevice touch);
		protected abstract void StartGesture(TouchDevice touch);
		protected abstract bool UpdateGesture(TouchDevice touch);
		protected abstract void EndGesture(TouchDevice device);

	}
}

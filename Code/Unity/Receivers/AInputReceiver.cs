namespace UnityPlugins.Input.Unity
{
	public abstract class AInputReceiver : IInputReceiver
	{
		public readonly InputLayerDefinition InputLayer;

		protected readonly DetectorUpdater m_Updater = new DetectorUpdater();
		private bool m_IsActive;

		public bool IsActive => m_IsActive;

		public AInputReceiver(InputLayerDefinition inputLayer)
		{
			InputLayer = inputLayer;
		}

		public abstract void UpdateInput();

		public void SetInputActive(bool active)
		{
			if(active != m_IsActive)
			{
				m_IsActive = active;
				if(active)
				{
					InputManager.Instance.Receivers.PushReceiver(this, m_Updater, InputLayer);
				}
				else
				{
					InputManager.Instance.Receivers.PopReceiver(this);
				}
			}
		}
	}
}

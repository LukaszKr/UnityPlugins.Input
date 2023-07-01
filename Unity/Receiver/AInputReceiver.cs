namespace ProceduralLevel.Input.Unity
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

		public void UpdateInput(InputManager inputManager)
		{
			OnUpdateInput(inputManager);
		}

		protected abstract void OnUpdateInput(InputManager inputManager);

		public void SetInputActive(InputManager inputManager, bool active)
		{
			if(active != m_IsActive)
			{
				m_IsActive = active;
				if(active)
				{
					inputManager.PushReceiver(this, m_Updater, InputLayer);
				}
				else
				{
					inputManager.PopReceiver(this);
				}
			}
		}
	}
}

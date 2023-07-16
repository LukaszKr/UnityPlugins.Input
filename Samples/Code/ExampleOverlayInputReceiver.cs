using ProceduralLevel.Input.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProceduralLevel.Input
{
	public class ExampleOverlayInputReceiver : MonoBehaviour, IInputReceiver
	{
		public readonly DetectorUpdater Updater;

		private AInputDetector m_TriggerTest;

		public ExampleOverlayInputReceiver()
		{
			m_TriggerTest = new TriggerDetector().Add(Key.Space);

			Updater = new DetectorUpdater(m_TriggerTest);
		}

		public void UpdateInput(InputManager inputManager)
		{
			if(m_TriggerTest.Active)
			{
				Debug.Log("CLOSE OVERLAY");
				inputManager.PopReceiver(this);
			}
		}
	}
}

using UnityEngine;

namespace UnityPlugins.Input.Unity
{
	public class InputManagerComponent : MonoBehaviour
	{
		public GameInputManager Manager => GameInputManager.Instance;

		private void OnDestroy()
		{
			GameInputManager.Instance.OnActiveDeviceChanged.RemoveAllListeners();
		}

		protected virtual void Update()
		{
			GameInputManager.Instance.Update(Time.deltaTime);
		}

		protected void DrawDebugGUI()
		{
			InputManagerDebugger.DrawDebugGUI(GameInputManager.Instance);
		}
	}
}

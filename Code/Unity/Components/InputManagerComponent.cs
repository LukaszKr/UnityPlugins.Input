using UnityEngine;

namespace UnityPlugins.Input.Unity
{
	public class InputManagerComponent : MonoBehaviour
	{
		public InputManager Manager => InputManager.Instance;

		private void OnDestroy()
		{
			InputManager.Instance.OnActiveDeviceChanged.RemoveAllListeners();
		}

		protected virtual void Update()
		{
			InputManager.Instance.Update(Time.deltaTime);
		}

		public void DrawDebugGUI()
		{
			InputManagerDebugger.DrawDebugGUI(InputManager.Instance);
		}
	}
}

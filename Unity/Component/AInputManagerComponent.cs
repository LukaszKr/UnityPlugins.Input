using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public abstract class AInputManagerComponent<TInputManager> : MonoBehaviour
		where TInputManager : InputManager, new()
	{
		public abstract TInputManager Manager { get; }

		private void OnDestroy()
		{
			Manager.OnActiveDeviceChanged.RemoveAllListeners();
		}

		protected virtual void Update()
		{
			Manager.Update(Time.deltaTime);
		}

		protected void DrawDebugGUI()
		{
			InputManagerDebugger.DrawDebugGUI(Manager);
		}
	}
}

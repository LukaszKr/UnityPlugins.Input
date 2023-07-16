using UnityEngine;

namespace ProceduralLevel.Input.Unity
{
	public abstract class AInputManagerComponent : MonoBehaviour
	{
		public abstract InputManager Manager { get; }

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

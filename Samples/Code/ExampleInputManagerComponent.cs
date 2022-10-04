using ProceduralLevel.UnityPlugins.Input.Unity;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input.Example
{
	public class ExampleInputManagerComponent : InputManagerComponent
	{
		private void OnGUI()
		{
			DrawDebugGUI();
			GUILayout.Label($"Mouse Delta: {MouseDevice.Instance.Delta}");
		}
	}
}
using ProceduralLevel.Input.Unity;
using UnityEngine;

namespace ProceduralLevel.Input.Example
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
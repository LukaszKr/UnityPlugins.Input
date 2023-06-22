using ProceduralLevel.Common.Ext;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public static class InputManagerDebugger
	{
		private static readonly List<AInputProvider> m_Providers = new List<AInputProvider>(64);

		#region Debug
		public static void DrawDebugGUI(InputManager manager)
		{
			TouchDevice touchDevice = TouchDevice.Instance;
			TouchData[] touches = touchDevice.Touches;
			int touchCount = touchDevice.Count;
			for(int x = 0; x < touchCount; ++x)
			{
				TouchData touch = touches[x];
				GUILayout.Label(touch.ToString());
			}

			manager.GetActiveProviders(m_Providers);
			if(m_Providers.Count > 0)
			{
				string str = StringExt.JoinToString(m_Providers);
				GUILayout.Label(str);
				m_Providers.Clear();
			}
		}
		#endregion
	}
}

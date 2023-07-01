using ProceduralLevel.Common.Ext;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ProceduralLevel.Input.Unity
{
	public static class InputManagerDebugger
	{
		private static readonly List<AInputProvider> m_Providers = new List<AInputProvider>(64);
		private static readonly StringBuilder m_StringBuilder = new StringBuilder();

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
				m_StringBuilder.Length = 0;
				int count = m_Providers.Count;
				for(int x = 0; x < count; x++)
				{
					if(x > 0)
					{
						m_StringBuilder.Append(", ");
					}
					AInputProvider provider = m_Providers[x];
					m_StringBuilder.Append(provider.ToString());
				}
				GUILayout.Label(m_StringBuilder.ToString());
				m_Providers.Clear();
			}
		}
		#endregion
	}
}

using ProceduralLevel.UnityPlugins.Common.Editor;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input.Editor
{
	public static class InputStyles
	{
		public static readonly ExtendedGUIStyle Active = new ExtendedGUIStyle("label");
		public static readonly ExtendedGUIStyle InActive = new ExtendedGUIStyle("label", (s) =>
		{
			s.normal.textColor = Color.red;
		});
	}
}

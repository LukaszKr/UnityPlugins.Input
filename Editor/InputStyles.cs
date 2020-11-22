using ProceduralLevel.UnityPluginsEditor.ExtendedEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPluginsEditor.Input
{
	public static class InputStyles
	{
		public static readonly ExtendedGUIStyle Active = new ExtendedGUIStyle("label");
		public readonly static ExtendedGUIStyle InActive = new ExtendedGUIStyle("label", (s) => {
			s.normal.textColor = Color.red;
		});
	}
}

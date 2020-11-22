using System;
using ProceduralLevel.UnityPlugins.Common;
using ProceduralLevel.UnityPlugins.Input;
using ProceduralLevel.UnityPluginsEditor.ExtendedEditor;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPluginsEditor.Input
{
	public class LayerDefinitionScrollView: ScrollView<LayerDefinition>
	{
		private float[] m_ColRatios = new float[] { 0.1f, 0.3f, 0.2f, 0.2f, 0.2f };
		private Rect[] m_Rects;

		private InputManager m_Manager;

		public LayerDefinitionScrollView(InputManager manager, string name) : base(manager.LayerDefinitions)
		{
			m_Rects = new Rect[m_ColRatios.Length];

			m_Manager = manager;
			AddHeader(new LabelScrollViewComponent<LayerDefinition>(name));
		}

		protected override float DrawItem(LayoutData layoutPosition, LayerDefinition entry, bool render)
		{
			float height = EditorGUIUtility.singleLineHeight;

			Rect position = new Rect(layoutPosition.X, layoutPosition.Y, layoutPosition.Width, height);
			if(render)
			{
				Type type = m_Manager.EnumIDType;
				position.SplitHorizontal(m_Rects, m_ColRatios);
				GUI.Label(m_Rects[0], "ID");
				if(type != null)
				{
					GUI.Label(m_Rects[1], Enum.GetName(type, entry.ID));
				}
				else
				{
					entry.ID = EditorGUI.IntField(m_Rects[1], entry.ID);
				}

				GUI.Label(m_Rects[2], "Priority");
				entry.Priority = EditorGUI.IntField(m_Rects[3], entry.Priority);
				entry.Block = EditorGUI.ToggleLeft(m_Rects[4], "Block", entry.Block);
			}
			return height;
		}

		protected override void OnContextClick(LayerDefinition entry, Vector2 clickPosition)
		{
			if(m_Manager.EnumIDType == null)
			{
				GenericMenu menu = new GenericMenu();
				menu.AddItem(new GUIContent("Delete"), false, () =>
				{
					m_Manager.LayerDefinitions.Remove(entry);
				});
				menu.ShowAsContext();
			}
		}
	}
}

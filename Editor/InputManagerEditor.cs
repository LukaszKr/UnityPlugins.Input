using System;
using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Input;
using ProceduralLevel.UnityPluginsEditor.ExtendedEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPluginsEditor.Input
{
	[CustomEditor(typeof(InputManager), true)]
	public class InputManagerEditor: AExtendedEditor<InputManager>
	{
		private const int LABEL_WIDTH = 100;

		private LayerDefinitionScrollView m_LayerScroll;

		protected override void Initialize()
		{
			m_LayerScroll = new LayerDefinitionScrollView(Target, nameof(InputManager.LayerDefinitions));
		}

		public override bool RequiresConstantRepaint()
		{
			return EditorApplication.isPlaying;
		}

		protected override void Draw()
		{
			DrawManagerSetup();
			DrawDefinitions();
			DrawActiveLayers();
			DrawDeviceStates();
		}

		private void DrawManagerSetup()
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

			if(GUILayout.Button("Update Layer List"))
			{
				UpdateDefinitionList();
			}
			EditorGUILayout.EndHorizontal();
		}

		private void DrawDefinitions()
		{
			Rect rect = GUILayoutUtility.GetRect(Width, 150);
			m_LayerScroll.Draw(rect);
		}

		private void DrawActiveLayers()
		{
			List<InputLayer> activeLayers = Target.GetActiveLayers();
			int count = (activeLayers != null? activeLayers.Count: 0);
			if(count > 0)
			{
				EditorGUILayout.BeginVertical("box");
				for(int x = 0; x != count; ++x)
				{
					InputLayer layer = activeLayers[x];
					DrawInputLayer(layer);
				}
				EditorGUILayout.EndVertical();
			}
			if(Target.EnumIDType == null)
			{
				if(GUILayout.Button("Add Layer"))
				{
					Target.LayerDefinitions.Add(new LayerDefinition(0, 0, false));
				}
			}
		}

		private void DrawInputLayer(InputLayer layer)
		{
			ExtendedGUIStyle style = (layer.IsActive? InputStyles.Active: InputStyles.InActive);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(layer.Receiver.ToString(), style);
			EditorGUILayout.LabelField(layer.Definition.Priority+":"+layer.Definition.Block, GUILayout.MaxWidth(64));
			EditorGUILayout.EndHorizontal();
		}

		private void DrawDeviceStates()
		{
			EditorGUILayout.LabelField("Active Device: "+Target.ActiveDevice.ToString());
			DrawDeviceInputState("Keyboard", Target.Keyboard, typeof(Key), 4);
			DrawMouseState(Target.Mouse);
			DrawTouchState(Target.Touch);
			DrawGamepadState("Any Gamepad", Target.AnyGamepad);
			DrawGamepadState("P1", Target.Gamepads[0]);
			DrawGamepadState("P2", Target.Gamepads[1]);
			DrawGamepadState("P3", Target.Gamepads[2]);
			DrawGamepadState("P4", Target.Gamepads[3]);
		}

		private void DrawTouchState(TouchDevice touch)
		{
			DrawDeviceInputState("Touch", touch, typeof(ETouchID), 3);
			int count = touch.Count;
			for(int x = 0; x < count; ++x)
			{
				TouchData unityTouch = touch.Touches[x];
				EditorGUILayout.LabelField($"{x}");
				EditorGUILayout.Vector2Field("Position", unityTouch.Position);
			}
		}

		private void DrawMouseState(MouseDevice mouse)
		{
			DrawDeviceInputState("Mouse", mouse, typeof(EMouseInputID), 3);
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.Vector2Field("Position", mouse.Position);
			EditorGUILayout.Vector2Field("Position Delta", mouse.PositionDelta);
			EditorGUILayout.Vector2Field("Delta", mouse.Delta);
			EditorGUILayout.Vector2Field("Scroll", mouse.Scroll);
			EditorGUI.EndDisabledGroup();
		}

		private void DrawGamepadState(string prefix, AGamepadDevice device)
		{
			DrawDeviceInputState($"{prefix} - {device.GamepadType}, Active: {device.IsActive}", device, typeof(EGamepadInputID), 3);
			EditorGUI.BeginDisabledGroup(true);
			DrawAxisPair(device, EGamepadInputID.LStickLeft, EGamepadInputID.LStickRight);
			DrawAxisPair(device, EGamepadInputID.LStickDown, EGamepadInputID.LStickUp);
			DrawAxisPair(device, EGamepadInputID.RStickLeft, EGamepadInputID.RStickRight);
			DrawAxisPair(device, EGamepadInputID.RStickDown, EGamepadInputID.RStickUp);
			DrawAxisPair(device, EGamepadInputID.LTrigger, EGamepadInputID.RTrigger);
			EditorGUI.EndDisabledGroup();
		}

		private void DrawAxisPair(AGamepadDevice device, EGamepadInputID a, EGamepadInputID b)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(a.ToString(), GUILayout.Width(LABEL_WIDTH));
			EditorGUILayout.FloatField(device.GetAxis(a));
			EditorGUILayout.LabelField(b.ToString(), GUILayout.Width(LABEL_WIDTH));
			EditorGUILayout.FloatField(device.GetAxis(b));
			EditorGUILayout.EndHorizontal();
		}

		private void DrawDeviceInputState(string name, AInputDevice device, Type enumType, int minLineCount)
		{
			InputState[] inputState = device.GetAllInputState();

			EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
			EditorGUILayout.BeginVertical(GUILayout.MinHeight(EditorStyles.label.TotalLineHeight()*minLineCount));
			if(device.AnyKeyPressed)
			{
				for(int x = 0; x < inputState.Length; ++x)
				{
					InputState state = inputState[x];
					if(state.Status > EInputStatus.Released)
					{
						EditorGUILayout.LabelField(string.Format("{0} -> {1}", Enum.GetName(enumType, x), state.ToString()));
					}
				}
			}
			else
			{
				EditorGUILayout.LabelField("-");
			}
			EditorGUILayout.EndVertical();
		}

		private void UpdateDefinitionList()
		{
			Type type = Target.EnumIDType;
			if(type != null)
			{
				Array values = Enum.GetValues(type);
				HashSet<int> validIDs = new HashSet<int>();
				for(int x = 0; x != values.Length; ++x)
				{
					validIDs.Add(values.GetValue(x).GetHashCode());
				}

				int count = Target.LayerDefinitions.Count;
				for(int x = count-1; x >= 0; --x)
				{
					LayerDefinition definition = Target.LayerDefinitions[x];
					if(!validIDs.Contains(definition.ID))
					{
						Target.LayerDefinitions.RemoveAt(x);
					}
					else
					{
						validIDs.Remove(definition.ID);
					}
				}

				foreach(int missingID in validIDs)
				{
					LayerDefinition definition = new LayerDefinition(missingID, 0, false);
					Target.LayerDefinitions.Add(definition);
				}

				Target.LayerDefinitions.Sort((a, b) => a.ID.CompareTo(b.ID));
			}
		}
	}
}

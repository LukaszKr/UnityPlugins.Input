﻿using System;
using System.Collections.Generic;
using ProceduralLevel.UnityPlugins.Common.Editor;
using ProceduralLevel.UnityPlugins.Input.Unity;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input.Editor
{
	[CustomEditor(typeof(InputManager), true)]
	public class InputManagerEditor : AExtendedEditor<InputManager>
	{
		private const int LABEL_WIDTH = 100;

		protected override void Initialize()
		{
		}

		public override bool RequiresConstantRepaint()
		{
			return EditorApplication.isPlaying;
		}

		protected override void Draw()
		{
			DrawActiveLayers();
			DrawDeviceStates();
		}

		private void DrawActiveLayers()
		{
			IReadOnlyList<InputLayer> activeLayers = Target.ActiveLayers;
			int count = (activeLayers != null? activeLayers.Count: 0);
			EditorGUILayout.BeginVertical("box");
			EditorGUILayout.LabelField($"Active Input Layers({count})", EditorStyles.boldLabel);
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
			EditorGUILayout.EndVertical();
		}

		private void DrawInputLayer(InputLayer layer)
		{
			ExtendedGUIStyle style = (layer.IsActive? InputStyles.Active: InputStyles.InActive);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(layer.Receiver.GetType().Name, style);
			InputLayerDefinition definition = layer.Definition;
			EditorGUILayout.LabelField($"{definition.Priority}:{definition.Block} - {definition.DebugName}", GUILayout.MaxWidth(192));
			EditorGUILayout.EndHorizontal();
		}

		private void DrawDeviceStates()
		{
			EditorGUILayout.BeginVertical("box");
			EditorGUILayout.LabelField("Active Device: "+Target.ActiveDevice.ToString());
			DrawDeviceInputState("Keyboard", KeyboardDevice.Instance, typeof(Key), 4);
			DrawMouseState(MouseDevice.Instance);
			DrawTouchState(TouchDevice.Instance);

			EGamepadID[] gamepadIDs = EGamepadIDExt.Meta.Values;
			int length = gamepadIDs.Length;
			for(int x = 0; x < length; ++x)
			{
				EGamepadID id = gamepadIDs[x];
				AGamepadDevice gamepad = id.GetGamepad();
				DrawGamepadState(id.ToString(), gamepad);
			}
			EditorGUILayout.EndVertical();
		}

		private void DrawTouchState(TouchDevice touch)
		{
			DrawDeviceInputState("Touch", touch, typeof(ETouchInputID), 3);
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
			EditorGUILayout.Vector2Field("Screen Delta", mouse.ScreenDelta);
			EditorGUILayout.Vector2Field("Raw Delta", mouse.RawDelta);
			EditorGUILayout.Vector2Field("Delta", mouse.Delta);
			EditorGUILayout.Vector2Field("Scroll", mouse.Scroll);
			EditorGUI.EndDisabledGroup();
		}

		private void DrawGamepadState(string prefix, AGamepadDevice device)
		{
			DrawDeviceInputState($"{prefix} Gamepad - {device.GamepadType}, Active: {device.IsActive}", device, typeof(EGamepadInputID), 3);
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
	}
}

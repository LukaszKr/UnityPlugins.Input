using System.Collections.Generic;
using ProceduralLevel.Common.Editor;
using ProceduralLevel.Input.Unity;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.Input.Editor
{
	[CustomEditor(typeof(AInputManagerComponent), true)]
	public class InputManagerEditor : AExtendedEditor<AInputManagerComponent>
	{
		private const int LABEL_WIDTH = 100;
		private readonly List<AInputProvider> m_Providers = new List<AInputProvider>();

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
			IReadOnlyList<InputLayer> activeLayers = Target.Manager.ActiveLayers;
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
			EditorGUILayout.LabelField("Active Device: "+Target.Manager.ActiveDevice.ToString());
			DrawDeviceInputState("Keyboard", KeyboardDevice.Instance, 4);
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
			DrawDeviceInputState("Touch", touch, 3);
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
			DrawDeviceInputState("Mouse", mouse, 3);
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
			DrawDeviceInputState($"{prefix} Gamepad - {device.GamepadType}, Active: {device.IsActive}", device, 3);
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

		private void DrawDeviceInputState(string name, ABaseInputDevice device, int minLineCount)
		{

			EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
			EditorGUILayout.BeginVertical(GUILayout.MinHeight(EditorStyles.label.TotalLineHeight()*minLineCount));
			if(device.IsActive)
			{
				device.GetActiveProviders(m_Providers);
				int count = m_Providers.Count;
				for(int x = 0; x < count; ++x)
				{
					AInputProvider provider = m_Providers[x];
					EditorGUILayout.LabelField(provider.ToString());
				}
				m_Providers.Clear();
			}
			else
			{
				EditorGUILayout.LabelField("-");
			}
			EditorGUILayout.EndVertical();
		}
	}
}

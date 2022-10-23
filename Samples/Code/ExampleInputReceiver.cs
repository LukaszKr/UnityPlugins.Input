using ProceduralLevel.UnityPlugins.Input.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input.Example
{
	public class ExampleInputReceiver : MonoBehaviour, IInputReceiver
	{
		[SerializeField]
		private ExampleInputManagerComponent m_InputManager = null;
		[SerializeField]
		private Transform m_RotateTarget = null;

		private DetectorUpdater m_Updater;

		private DurationDetector m_ShortcutA;
		private DurationDetector m_ShortcutB;
		private IntervalDetector m_Interval;

		private DurationDetector m_RotateUp;
		private DurationDetector m_RotateDown;
		private DurationDetector m_RotateLeft;
		private DurationDetector m_RotateRight;

		private DurationDetector m_CloneA;
		private DurationDetector m_CloneB;

		private void Awake()
		{
			m_CloneA = new DurationDetector().Add(Key.Digit1);
			m_CloneB = new DurationDetector().Add(Key.Digit1);

			m_ShortcutA = new DurationDetector();
			m_ShortcutA.Add(new ShortcutProvider().Add(Key.LeftCtrl).Add(EMouseInputID.Left));
			m_ShortcutA.Add(new ShortcutProvider().Add(Key.LeftCtrl).Add(Key.A));
			m_ShortcutB = new DurationDetector();
			m_ShortcutB.Add(Key.LeftCtrl);

			m_Interval = new IntervalDetector(0f, 0.5f, 0.4f, 0.3f, 0.2f)
				.Add(Key.I);

			m_RotateUp = new DurationDetector().Add(
				new ShortcutProvider().Add(EMouseInputID.MoveDown).Add(EMouseInputID.Left)
			).Add(Key.W);
			m_RotateDown = new DurationDetector().Add(
				new ShortcutProvider().Add(EMouseInputID.MoveUp).Add(EMouseInputID.Left)
			).Add(Key.S);
			m_RotateLeft = new DurationDetector().Add(
				new ShortcutProvider().Add(EMouseInputID.MoveLeft).Add(EMouseInputID.Left)
			).Add(Key.A);
			m_RotateRight = new DurationDetector().Add(
				new ShortcutProvider().Add(EMouseInputID.MoveRight).Add(EMouseInputID.Left)
			).Add(Key.D);

			m_Updater = new DetectorUpdater(
				m_CloneA, m_CloneB,
				m_ShortcutB, m_ShortcutA, m_Interval,
				m_RotateUp, m_RotateDown, m_RotateLeft, m_RotateRight
			);
		}

		private void OnEnable()
		{
			m_InputManager.Manager.PushReceiver(this, m_Updater, new InputLayerDefinition("Example", 0, false));
		}

		private void OnDisable()
		{
			m_InputManager.Manager.PopReceiver(this);
		}

		public void UpdateInput(InputManager inputManager)
		{
			if(m_CloneA.Active)
			{
				Debug.Log("Priority A");
			}
			if(m_CloneB.Active)
			{
				Debug.Log("Priority B");
			}

			if(m_ShortcutA.Active)
			{
				Debug.Log("SHORTCUT A");
			}
			if(m_ShortcutB.Active)
			{
				Debug.Log("SHORTCUT B");
			}
			if(m_Interval.Active)
			{
				Debug.Log($"INTERVAL: {m_Interval.CurrentInterval}:{m_Interval.Count}");
			}

			if(m_RotateUp.Active)
			{
				m_RotateTarget.Rotate(-m_RotateUp.Axis, 0f, 0f, Space.World);
			}
			if(m_RotateDown.Active)
			{
				m_RotateTarget.Rotate(m_RotateDown.Axis, 0f, 0f, Space.World);

			}
			if(m_RotateLeft.Active)
			{
				m_RotateTarget.Rotate(0f, m_RotateLeft.Axis, 0f, Space.World);

			}
			if(m_RotateRight.Active)
			{
				m_RotateTarget.Rotate(0f, -m_RotateRight.Axis, 0f, Space.World);
			}
		}
	}
}

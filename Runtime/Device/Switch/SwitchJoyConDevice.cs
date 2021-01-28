#if UNITY_SWITCH
using nn.hid;
#endif
using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class SwitchJoyConDevice: AInputDevice
	{
		private const int INPUT_COUNT = 28;

#if UNITY_SWITCH
		private NpadId m_ID = NpadId.Invalid;
		private NpadStyle m_Style = NpadStyle.Invalid;
		private NpadState m_State = new NpadState();
#endif

		public int Tick = 0;

		public SwitchJoyConDevice()
			: base(EDeviceID.SwitchGamepad, INPUT_COUNT)
		{
		}

		public void Initialize()
		{
#if UNITY_SWITCH
			Npad.Initialize();
			Npad.SetSupportedIdType(new NpadId[] { NpadId.Handheld, NpadId.No1 });
			Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
#endif
		}

		#region Getters
		public InputState Get(EJoyConInputID button)
		{
			return m_InputState[(int)button];
		}
		#endregion

		#region Update State
		protected override void OnUpdateState(InputManager inputManager)
		{
			++Tick;
#if UNITY_SWITCH
			NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
			NpadState handheldState = m_State;
			if(handheldStyle != NpadStyle.None)
			{
				Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);
				if(handheldState.buttons != NpadButton.None)
				{
					m_ID = NpadId.Handheld;
					m_Style = handheldStyle;
					m_State = handheldState;
					return;
				}
			}

			NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
			NpadState no1State = m_State;
			if(no1Style != NpadStyle.None)
			{
				Npad.GetState(ref no1State, NpadId.No1, no1Style);
				if(no1State.buttons != NpadButton.None)
				{
					m_ID = NpadId.No1;
					m_Style = no1Style;
					m_State = no1State;
					return;
				}
			}

			if((m_ID == NpadId.Handheld) && (handheldStyle != NpadStyle.None))
			{
				m_ID = NpadId.Handheld;
				m_Style = handheldStyle;
				m_State = handheldState;
			}
			else if((m_ID == NpadId.No1) && (no1Style != NpadStyle.None))
			{
				m_ID = NpadId.No1;
				m_Style = no1Style;
				m_State = no1State;
			}
			else
			{
				m_ID = NpadId.Invalid;
				m_Style = NpadStyle.Invalid;
				m_State.Clear();
			}
#endif
		}

		protected override RawInputState GetRawState(int inputID)
		{
#if UNITY_SWITCH
			bool isPressed = m_State.GetButton((NpadButton)(1 << inputID));
			return new RawInputState(isPressed);
#else
			return new RawInputState(false);
#endif
		}
		#endregion

		public override void RecordProviders(List<AInputProvider> providers)
		{
			if(IsActive)
			{
				int length = m_InputState.Length;
				for(int x = 0; x < length; ++x)
				{
					InputState state = m_InputState[x];
					if(state.IsActive)
					{
						providers.Add(new JoyConProvider((EJoyConInputID)x));
					}
				}
			}
		}
	}
}
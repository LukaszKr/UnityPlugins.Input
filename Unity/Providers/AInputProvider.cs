using System;

namespace UnityPlugins.Input.Unity
{
	public abstract class AInputProvider : IComparable<AInputProvider>
	{
		private InputState m_State;
		private int m_LastUpdateTick;

		public InputState State => m_State;

		public void Update(int updateTick)
		{
			int delta = updateTick-m_LastUpdateTick;
			m_LastUpdateTick = updateTick;
			switch(delta)
			{
				case 0:
					return; //already updated in this frame
				case 1:
					m_State = m_State.Combine(GetRawState());
					break;
				default: //skipped update frame, reset state
					m_State = new InputState(EInputStatus.Released);
					break;
			}
		}

		public abstract RawInputState GetRawState();

		public int CompareTo(AInputProvider other)
		{
			if(other == this)
			{
				return 0;
			}
			Type thisType = GetType();
			Type otherType = other.GetType();
			if(thisType != otherType)
			{
				return thisType.Name.CompareTo(otherType.Name);
			}
			return OnCompareTo(other);
		}

		protected abstract int OnCompareTo(AInputProvider other);

		public override string ToString()
		{
			return $"[{GetType().Name} | {ToStringImpl()}]";
		}

		protected abstract string ToStringImpl();
	}
}

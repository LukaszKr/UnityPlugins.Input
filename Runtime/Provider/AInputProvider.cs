using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputProvider: IComparable<AInputProvider>
	{
		protected int m_UpdateTick = 0;
		private RawInputState m_State;

		public RawInputState State { get { return m_State; } }

		public RawInputState UpdateState(int updateTick)
		{
			int oldTick = m_UpdateTick;
			m_UpdateTick = updateTick;
			if(oldTick == m_UpdateTick)
			{
				return m_State;
			}
			if(oldTick != m_UpdateTick-1)
			{
				m_State = new RawInputState(false);
			}
			else
			{
				m_State = GetState();
			}
			return m_State;
		}

		protected abstract RawInputState GetState();

		public abstract bool Contains(AInputProvider provider);

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

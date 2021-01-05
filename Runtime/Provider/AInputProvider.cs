using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputProvider: IComparable<AInputProvider>
	{
		private int m_UpdateTick = 0;
		private RawInputState m_State;

		public RawInputState State { get { return m_State; } }

		public RawInputState UpdateState(InputManager inputManager)
		{
			int oldTick = m_UpdateTick;
			m_UpdateTick = inputManager.UpdateTick;
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
				m_State = GetState(inputManager);
			}
			return m_State;
		}

		protected abstract RawInputState GetState(InputManager inputManager);

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

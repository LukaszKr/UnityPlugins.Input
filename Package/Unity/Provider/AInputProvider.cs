﻿using System;

namespace ProceduralLevel.Input.Unity
{
	public abstract class AInputProvider : IComparable<AInputProvider>
	{
		protected int m_UpdateTick = 0;
		private InputState m_State;

		public InputState State => m_State;

		public InputState UpdateState(int updateTick)
		{
			int oldTick = m_UpdateTick;
			m_UpdateTick = updateTick;
			if(oldTick == m_UpdateTick)
			{
				return m_State;
			}
			if(oldTick != m_UpdateTick-1)
			{
				m_State = new InputState();
			}
			else
			{
				m_State = GetState();
			}
			return m_State;
		}

		protected abstract InputState GetState();

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

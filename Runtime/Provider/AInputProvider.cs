using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputProvider: IComparable<AInputProvider>
	{
		private int m_UpdateTick = 0;

		public RawInputState GetState(InputManager inputManager)
		{
			int oldTick = m_UpdateTick;
			m_UpdateTick = inputManager.UpdateTick;
			if(oldTick != m_UpdateTick-1)
			{
				return new RawInputState(false);
			}
			return OnGetState(inputManager);
		}

		protected abstract RawInputState OnGetState(InputManager inputManager);

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

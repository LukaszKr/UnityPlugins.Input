﻿namespace ProceduralLevel.UnityPlugins.Input
{
	public struct RawInputState
	{
		public bool IsActive;
		public readonly bool IsRealAxis;
		public readonly float Axis;

		public RawInputState(bool isActive)
		{
			IsActive = isActive;
			IsRealAxis = false;
			Axis = (IsActive ? 1f : 0f);
		}

		public RawInputState(bool isActive, float axis)
		{
			IsActive = isActive;
			IsRealAxis = true;
			Axis = axis;
		}

		public RawInputState(InputState state)
		{
			IsActive = (EInputStatus.IsDown.Contains(state.Status));
			IsRealAxis = state.IsRealAxis;
			Axis = state.Axis;
		}

		public override string ToString()
		{
			return $"({nameof(IsActive)}: {IsActive}, {nameof(IsRealAxis)}: {IsRealAxis}, {nameof(Axis)}: {Axis})";
		}
	}
}
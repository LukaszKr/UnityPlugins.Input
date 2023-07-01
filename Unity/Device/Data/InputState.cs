namespace ProceduralLevel.Input.Unity
{
	public struct InputState
	{
		public readonly bool IsActive;
		public readonly bool IsRealAxis;
		public readonly float Axis;

		public InputState(bool isActive)
		{
			IsActive = isActive;
			IsRealAxis = false;
			Axis = (IsActive ? 1f : 0f);
		}

		public InputState(bool isActive, float axis, bool isRealAxis = true)
		{
			IsActive = isActive;
			IsRealAxis = isRealAxis;
			Axis = axis;
		}

		public override string ToString()
		{
			return $"({nameof(IsActive)}: {IsActive}, {nameof(IsRealAxis)}: {IsRealAxis}, {nameof(Axis)}: {Axis})";
		}
	}
}
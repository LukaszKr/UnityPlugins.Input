namespace ProceduralLevel.Input.Unity
{
	public struct RawInputState
	{
		public readonly bool IsActive;
		public readonly bool IsRealAxis;
		public readonly float Axis;

		public RawInputState(bool isActive)
		{
			IsActive = isActive;
			IsRealAxis = false;
			Axis = (isActive ? 1f : 0f);
		}

		public RawInputState(bool isActive, float axis, bool isRealAxis = true)
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
namespace UnityPlugins.Input.Unity
{
	public readonly struct InputState
	{
		public readonly EInputStatus Status;
		public readonly bool IsRealAxis;
		public readonly float Axis;

		public readonly bool IsActive => Status.IsPressed();

		public InputState(EInputStatus status)
		{
			Status = status;
			IsRealAxis = false;
			Axis = (status.IsPressed() ? 1f : 0f);
		}

		public InputState(EInputStatus status, float axis, bool isRealAxis = true)
		{
			Status = status;
			IsRealAxis = isRealAxis;
			Axis = axis;
		}

		public InputState Combine(RawInputState rawData)
		{
			return new InputState(Status.GetNext(rawData.IsActive), rawData.Axis, rawData.IsRealAxis);
		}

		public override string ToString()
		{
			return $"({nameof(Status)}: {Status}, {nameof(IsRealAxis)}: {IsRealAxis}, {nameof(Axis)}: {Axis})";
		}
	}
}
namespace ProceduralLevel.UnityPlugins.Input
{
	public struct InputState
	{
		public readonly EInputStatus Status;
		public readonly bool IsRealAxis;
		public readonly float Axis;

		public bool IsActive { get { return EInputStatus.IsDown.Contains(Status); } }

		public InputState(EInputStatus status)
		{
			Status = status;
			IsRealAxis = false;
			Axis = (EInputStatus.IsDown.Contains(status)? 1f: 0f);
		}

		public InputState(EInputStatus status, float axis)
		{
			Status = status;
			IsRealAxis = true;
			Axis = axis;
		}

		private InputState(EInputStatus status, float axis, bool isRealAxis)
		{
			Status = status;
			IsRealAxis = isRealAxis;
			Axis = axis;
		}

		public InputState GetNextState(RawInputState rawInput)
		{
			EInputStatus newStatus = Status.GetNextStatus(rawInput.IsActive);
			return new InputState(newStatus, rawInput.Axis, rawInput.IsRealAxis);
		}

		public RawInputState ToRaw()
		{
			return new RawInputState(this);
		}

		public override string ToString()
		{
			return $"({nameof(Status)}: {Status}, {nameof(IsRealAxis)}: {IsRealAxis}, {nameof(Axis)}: {Axis})";
		}
	}
}

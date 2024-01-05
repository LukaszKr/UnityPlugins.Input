using UnityEngine.InputSystem.Controls;

namespace ProceduralLevel.Input.Unity
{
	public struct InputState
	{
		public readonly EInputStatus Status;
		public readonly bool IsRealAxis;
		public readonly float Axis;

		public bool IsActive => Status.IsPressed();

		public InputState(ButtonControl control)
		{
			IsRealAxis = false;
			if(control.isPressed)
			{
				Axis = 1f;
				if(control.wasPressedThisFrame)
				{
					Status = EInputStatus.JustPressed;
				}
				else
				{
					Status = EInputStatus.Pressed;
				}
			}
			else
			{
				Axis = 0f;
				if(control.wasReleasedThisFrame)
				{
					Status = EInputStatus.JustReleased;
				}
				else
				{
					Status = EInputStatus.Released;
				}
			}
		}

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

		public InputState Combine(RawInputState rawState)
		{
			return new InputState(Status.GetNext(rawState.IsActive), rawState.Axis, rawState.IsRealAxis);
		}

		public override string ToString()
		{
			return $"({nameof(Status)}: {Status}, {nameof(IsRealAxis)}: {IsRealAxis}, {nameof(Axis)}: {Axis})";
		}
	}
}
namespace ProceduralLevel.UnityPlugins.Input
{
	public struct InputProviderState
	{
		public readonly bool Triggered;
		public readonly bool IsRealAxis;
		public readonly float Axis;

		public InputProviderState(bool triggered)
		{
			Triggered = triggered;
			IsRealAxis = false;
			Axis = (triggered? 1f: 0f);
		}

		public InputProviderState(bool triggered, float axis)
		{
			if(axis < 0)
			{
				axis = -axis;
			}
			Triggered = triggered;
			IsRealAxis = true;
			Axis = axis;
		}

		public override string ToString()
		{
			return string.Format("({0}, {1})",
				Triggered.ToString(), Axis.ToString());
		}
	}
}

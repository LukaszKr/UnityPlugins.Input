namespace ProceduralLevel.UnityPlugins.Input
{
	public struct InputProviderData
	{
		public readonly bool Triggered;
		public readonly float Axis;
		public readonly bool IsRealAxis;

		public InputProviderData(bool triggered)
		{
			Triggered = triggered;
			Axis = (triggered? 1f: 0f);
			IsRealAxis = false;
		}

		public InputProviderData(bool triggered, float axis)
		{
			if(axis < 0)
			{
				axis = -axis;
			}
			Triggered = triggered;
			Axis = axis;
			IsRealAxis = true;
		}

		public override string ToString()
		{
			return string.Format("({0}, {1})",
				Triggered.ToString(), Axis.ToString());
		}
	}
}

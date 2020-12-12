namespace ProceduralLevel.UnityPlugins.Input
{
	public struct InputProviderData
	{
		public readonly bool Triggered;
		public readonly float Axis;

		public InputProviderData(bool triggered)
		{
			Triggered = triggered;
			Axis = (triggered? 1f: 0f);
		}

		public InputProviderData(bool triggered, float axis)
		{
			if(axis < 0)
			{
				axis = -axis;
			}
			Triggered = triggered;
			Axis = axis;
		}

		public override string ToString()
		{
			return string.Format("({0}, {1})",
				Triggered.ToString(), Axis.ToString());
		}
	}
}

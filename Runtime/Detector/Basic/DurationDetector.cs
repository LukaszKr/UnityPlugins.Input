namespace ProceduralLevel.UnityPlugins.Input
{
	public class DurationDetector: AInputDetector
	{
		public float Duration { get; private set; }

		protected override bool OnInputUpdate(InputManager inputManager)
		{
			Duration += inputManager.DeltaTime;
			return true;
		}

		protected override void OnInputReset()
		{
			Duration = 0f;
		}

		public override string ToString()
		{
			return base.ToString()+string.Format("[Duration: {0}]", Duration);
		}
	}
}

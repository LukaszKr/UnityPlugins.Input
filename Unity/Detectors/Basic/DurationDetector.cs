namespace UnityPlugins.Input.Unity
{
	public class DurationDetector : AInputDetector
	{
		public float Duration { get; private set; }

		protected override bool OnInputUpdate(InputState inputState, float deltaTime)
		{
			Duration += deltaTime;
			return true;
		}

		protected override void OnResetState()
		{
			Duration = 0f;
		}

		public override string ToString()
		{
			return base.ToString()+$"[{nameof(Duration)}: {Duration}]";
		}
	}
}

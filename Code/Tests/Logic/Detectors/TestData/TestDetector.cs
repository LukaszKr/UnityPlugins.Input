namespace UnityPlugins.Input.Unity.Detectors
{
	public class TestDetector : AInputDetector
	{
		public int ResetStateCallCount;
		public int OnInputUpdateCallCount;

		protected override bool OnInputUpdate(InputState inputState, float deltaTime)
		{
			OnInputUpdateCallCount++;
			return inputState.IsActive;
		}

		protected override void OnResetState()
		{
			ResetStateCallCount++;
		}
	}
}

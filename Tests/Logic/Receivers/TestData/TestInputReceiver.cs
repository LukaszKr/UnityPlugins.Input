namespace UnityPlugins.Input.Unity.Receivers
{
	public class TestInputReceiver : IInputReceiver
	{
		public readonly DetectorUpdater Updater = new DetectorUpdater();

		public int UpdateCount;

		public void UpdateInput()
		{
			UpdateCount++;
		}
	}
}

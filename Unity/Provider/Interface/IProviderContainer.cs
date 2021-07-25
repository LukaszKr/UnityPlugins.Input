namespace ProceduralLevel.UnityPlugins.Input.Unity
{
	public interface IProviderContainer
	{
		void AddProvider(AInputProvider provider);
		void Sort();
	}

}

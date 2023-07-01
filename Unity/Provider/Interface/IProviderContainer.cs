namespace ProceduralLevel.Input.Unity
{
	public interface IProviderContainer
	{
		void AddProvider(AInputProvider provider);
		void Sort();
	}

}

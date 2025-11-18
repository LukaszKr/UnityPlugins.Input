namespace UnityPlugins.Input.Unity
{
	public interface IGroupProvider
	{
		void Add(AInputProvider provider);
		void Sort();
	}
}

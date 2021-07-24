namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class ADeviceInputProvider : AInputProvider
	{
		public override bool Contains(AInputProvider provider)
		{
			if(GetType() == provider.GetType())
			{
				return OnCompareTo(provider) == 0;
			}
			return false;
		}
	}
}

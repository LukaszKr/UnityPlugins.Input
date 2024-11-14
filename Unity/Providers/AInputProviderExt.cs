using System.Collections.Generic;

namespace UnityPlugins.Input.Unity
{
	public static class AInputProviderExt
	{
		public static void SortProviders(this List<AInputProvider> list)
		{
			int count = list.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = list[x];
				IGroupProvider container = provider as IGroupProvider;
				if(container != null)
				{
					container.Sort();
				}
			}
			list.Sort();
		}
	}
}

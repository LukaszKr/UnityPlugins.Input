using System;
using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class CombinationProvider: AInputProvider
	{
		public readonly List<AInputProvider> Providers = new List<AInputProvider>();

		protected override InputProviderData OnRefresh(InputManager inputManager)
		{
			float axis = 0f;

			int count = Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = Providers[x];
				InputProviderData data = provider.Refresh(inputManager);
				if(!data.Triggered)
				{
					return new InputProviderData(false, 0f);
				}
				axis = Math.Max(data.Axis, axis);
			}
			return new InputProviderData(true, axis);
		}
	}
}

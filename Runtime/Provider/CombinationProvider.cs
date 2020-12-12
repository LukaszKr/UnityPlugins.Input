using System;
using System.Collections.Generic;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class CombinationProvider: AInputProvider
	{
		public readonly List<AInputProvider> Providers = new List<AInputProvider>();

		protected override InputProviderState OnRefresh(InputManager inputManager)
		{
			float axis = 0f;
			bool isRealAxis = false;

			int count = Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = Providers[x];
				InputProviderState data = provider.Refresh(inputManager);
				if(!data.Triggered)
				{
					return new InputProviderState(false, 0f);
				}
				//in case of key+axis combination, we want to return axis value
				if(data.IsRealAxis)
				{
					isRealAxis = true;
					axis = Math.Max(data.Axis, axis);
				}
			}

			if(isRealAxis)
			{
				return new InputProviderState(true, axis);
			}
			return new InputProviderState(true);
		}
	}
}

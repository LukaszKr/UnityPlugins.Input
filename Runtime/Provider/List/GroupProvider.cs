using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	//OR-List
	public class GroupProvider: AListProvider
	{

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			float axis = 0f;
			bool isRealAxis = false;
			bool isAnyProviderActive = false;

			int count = Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = Providers[x];
				RawInputState data = provider.GetState(inputManager);
				if(data.IsActive)
				{
					isAnyProviderActive = true;

					if(data.IsRealAxis)
					{
						isRealAxis = true;
						axis = Math.Max(data.Axis, axis);
					}
				}
			}

			return new RawInputState(isAnyProviderActive, axis, isRealAxis);
		}
	}
}

﻿using System;
using System.Collections.Generic;
using ProceduralLevel.Common.Ext;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class AndListProvider: AListProvider
	{
		protected override RawInputState OnGetState(InputManager inputManager)
		{
			float axis = 0f;
			bool isRealAxis = false;

			int count = Providers.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = Providers[x];
				RawInputState data = provider.GetState(inputManager);
				if(!data.IsActive)
				{
					return new RawInputState(false);
				}
				//in case of key+axis combination, we want to return axis value
				if(data.IsRealAxis)
				{
					isRealAxis = true;
					axis = Math.Max(data.Axis, axis);
				}
			}

			return new RawInputState(true, axis, isRealAxis);
		}
	}
}
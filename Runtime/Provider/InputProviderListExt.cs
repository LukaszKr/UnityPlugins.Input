using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public static class InputProviderListExt
	{
		public static List<AInputProvider> Add(this List<AInputProvider> providers, AInputProvider provider)
		{
			providers.Add(provider);
			return providers;
		}

		public static List<AInputProvider> Add(this List<AInputProvider> providers, Key key)
		{
			providers.Add(new KeyboardProvider(key));
			return providers;
		}

		public static List<AInputProvider> Add(this List<AInputProvider> providers, EMouseButton button)
		{
			providers.Add(new MouseProvider(button));
			return providers;
		}

		public static List<AInputProvider> Add(this List<AInputProvider> providers, EGamepadButton button, EGamepadID gamepadID = EGamepadID.Any)
		{
			providers.Add(new GamepadProvider(button, gamepadID));
			return providers;
		}

		public static List<AInputProvider> Add(this List<AInputProvider> providers, ETouchID touchID)
		{
			providers.Add(new TouchProvider(touchID));
			return providers;
		}

		public static List<AInputProvider> AddTouchCount(this List<AInputProvider> providers, int count)
		{
			providers.Add(new TouchCountProvider(count));
			return providers;
		}
	}
}

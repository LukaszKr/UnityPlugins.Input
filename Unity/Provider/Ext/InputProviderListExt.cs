using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ProceduralLevel.Input.Unity
{
	public static class InputProviderListExt
	{
		public static void SortProviders(this List<AInputProvider> list)
		{
			int count = list.Count;
			for(int x = 0; x < count; ++x)
			{
				AInputProvider provider = list[x];
				IProviderContainer container = provider as IProviderContainer;
				if(container != null)
				{
					container.Sort();
				}
			}
			list.Sort();
		}

		public static TSource Add<TSource>(this TSource source, AInputProvider provider)
			where TSource : IProviderContainer
		{
			source.AddProvider(provider);
			return source;
		}

		public static TSource Add<TSource>(this TSource source, Key key)
			where TSource : IProviderContainer
		{
			source.AddProvider(new KeyboardProvider(key));
			return source;
		}

		public static TSource Add<TSource>(this TSource source, EMouseInputID button)
			where TSource : IProviderContainer
		{
			source.AddProvider(new MouseProvider(button));
			return source;
		}

		public static TSource Add<TSource>(this TSource source, EGamepadInputID button, EGamepadID gamepadID = EGamepadID.Any)
			where TSource : IProviderContainer
		{
			source.AddProvider(new GamepadProvider(button, gamepadID));
			return source;
		}

		public static TSource Add<TSource>(this TSource source, ETouchInputID touchID)
			where TSource : IProviderContainer
		{
			source.AddProvider(new TouchProvider(touchID));
			return source;
		}

		public static TSource AddTouchCount<TSource>(this TSource source, int count)
			where TSource : IProviderContainer
		{
			source.AddProvider(new TouchCountProvider(count));
			return source;
		}
	}
}

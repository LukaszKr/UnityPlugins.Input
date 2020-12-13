using UnityEngine.InputSystem;

namespace ProceduralLevel.UnityPlugins.Input
{
	public static class InputProviderListExt
	{
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

		public static TSource Add<TSource>(this TSource source, EMouseButton button)
			where TSource : IProviderContainer
		{
			source.AddProvider(new MouseProvider(button));
			return source;
		}

		public static TSource Add<TSource>(this TSource source, EGamepadButton button, EGamepadID gamepadID = EGamepadID.Any)
			where TSource : IProviderContainer
		{
			source.AddProvider(new GamepadProvider(button, gamepadID));
			return source;
		}

		public static TSource Add<TSource>(this TSource source, ETouchID touchID)
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

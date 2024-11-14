using UnityEngine.InputSystem;

namespace UnityPlugins.Input.Unity
{
	public static class InputProviderListExt
	{
		public static TSource Add<TSource>(this TSource source, AInputProvider provider)
			where TSource : IGroupProvider
		{
			source.Add(provider);
			return source;
		}

		public static TSource Add<TSource>(this TSource source, Key key)
			where TSource : IGroupProvider
		{
			source.Add(new KeyboardProvider(key));
			return source;
		}

		public static TSource Add<TSource>(this TSource source, EMouseInputID button)
			where TSource : IGroupProvider
		{
			source.Add(new MouseProvider(button));
			return source;
		}

		public static TSource Add<TSource>(this TSource source, EGamepadInputID button, EGamepadID gamepadID = EGamepadID.Any)
			where TSource : IGroupProvider
		{
			source.Add(new GamepadProvider(button, gamepadID));
			return source;
		}

		public static TSource Add<TSource>(this TSource source, ETouchInputID touchID)
			where TSource : IGroupProvider
		{
			source.Add(new TouchProvider(touchID));
			return source;
		}

		public static TSource AddTouchCount<TSource>(this TSource source, int count)
			where TSource : IGroupProvider
		{
			source.Add(new TouchCountProvider(count));
			return source;
		}
	}
}

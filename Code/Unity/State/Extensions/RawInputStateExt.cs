using UnityEngine.InputSystem.Controls;
namespace UnityPlugins.Input.Unity
{
	public static class RawInputStateExt
	{
		public static RawInputState ToRawInputState(this ButtonControl control)
		{
			if(control.isPressed)
			{
				return new RawInputState(1f, false);
			}
			else
			{
				return new RawInputState(false);
			}
		}
	}
}

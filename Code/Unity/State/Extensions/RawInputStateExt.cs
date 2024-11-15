using UnityEngine.InputSystem.Controls;
namespace UnityPlugins.Input.Unity
{
	public static class RawInputStateExt
	{
		public static RawInputState ToRawInputState(this ButtonControl control)
		{
			bool isRealAxis = false;
			float axis;

			if(control.isPressed)
			{
				axis = 1f;
				return new RawInputState(axis, isRealAxis);
			}
			else
			{
				return new RawInputState(false);
			}
		}
	}
}

namespace ProceduralLevel.Input.Unity
{
	public class PinchGestureProvider : ADeviceInputProvider
	{
		public PinchGestureProvider()
		{
		}

		protected override InputState GetState()
		{
			TouchDevice touch = TouchDevice.Instance;
			if(touch.Count == 2)
			{
				//TouchData touch1 = touch.Touches[0];
				//TouchData touch2 = touch.Touches[1];
				//Vector2 deltaA = touch.Touches[0].Delta;
				//Vector2 deltaB = touch.Touches[1].Delta;
				//Vector2 delta = deltaA+deltaB;
			}
			return new InputState(false, 0f);
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			return 0;
		}

		protected override string ToStringImpl()
		{
			return string.Empty;
		}
	}
}

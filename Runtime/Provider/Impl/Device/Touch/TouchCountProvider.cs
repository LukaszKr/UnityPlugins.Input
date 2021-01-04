﻿namespace ProceduralLevel.UnityPlugins.Input
{
	public class TouchCountProvider: AInputProvider
	{
		public int Count;

		public TouchCountProvider()
		{
		}

		public TouchCountProvider(int count)
		{
			Count = count;
		}

		protected override RawInputState OnGetState(InputManager inputManager)
		{
			return new RawInputState(inputManager.Touch.Count == Count);
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			TouchCountProvider otherProvider = (TouchCountProvider)other;
			return Count.CompareTo(otherProvider.Count);
		}

		protected override string ToStringImpl()
		{
			return $"{nameof(Count)}: {Count}";
		}
	}
}

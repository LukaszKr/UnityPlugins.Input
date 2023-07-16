﻿using UnityEngine;

namespace ProceduralLevel.Input.Unity
{
	public class DurationDetector : AInputDetector
	{
		public float Duration { get; private set; }

		protected override bool OnInputUpdate(InputState inputState, float deltaTime)
		{
			Duration += deltaTime;
			return true;
		}

		protected override void OnInputReset()
		{
			Duration = 0f;
		}

		public override string ToString()
		{
			return base.ToString()+$"[{nameof(Duration)}: {Duration}]";
		}
	}
}

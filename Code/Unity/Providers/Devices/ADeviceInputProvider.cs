using System;
using UnityPlugins.Common.Logic;

namespace UnityPlugins.Input.Unity
{
	public abstract class ADeviceInputProvider<TProvider, TInput> : AInputProvider
		where TProvider : ADeviceInputProvider<TProvider, TInput>
		where TInput : Enum
	{
		protected TInput m_InputID;

		public TInput InputID => m_InputID;

		public readonly CustomEvent<TProvider> OnChanged = new CustomEvent<TProvider>();

		public ADeviceInputProvider()
		{

		}

		public ADeviceInputProvider(TInput input)
		{
			m_InputID = input;
		}

		public void SetInput(TInput inputID)
		{
			if(Equals(m_InputID, inputID))
			{
				return;
			}

			m_InputID = inputID;
			OnChanged.Invoke((TProvider)this);
		}

		protected override int OnCompareTo(AInputProvider other)
		{
			ADeviceInputProvider<TProvider, TInput> otherProvider = (ADeviceInputProvider<TProvider, TInput>)other;
			return m_InputID.CompareTo(otherProvider.m_InputID);
		}

		protected override string ToStringImpl()
		{
			return $"{m_InputID}";
		}
	}
}

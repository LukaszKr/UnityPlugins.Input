namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputProvider
	{
		public readonly EDeviceID DeviceID;

		private int m_UpdateTick = 0;

		protected AInputProvider(EDeviceID deviceID)
		{
			DeviceID = deviceID;
		}

		public RawInputState GetState(InputManager inputManager)
		{
			int oldTick = m_UpdateTick;
			m_UpdateTick = inputManager.UpdateTick;
			if(oldTick != m_UpdateTick-1)
			{
				return new RawInputState(false);
			}
			return OnGetState(inputManager);
		}

		protected abstract RawInputState OnGetState(InputManager inputManager);

		public override string ToString()
		{
			return $"[{DeviceID} | {ToStringImpl()}]";
		}

		protected abstract string ToStringImpl();
	}
}

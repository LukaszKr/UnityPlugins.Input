namespace ProceduralLevel.UnityPlugins.Input
{
	public abstract class AInputLink
	{
		public readonly EDeviceID ID;

		public AInputLink(EDeviceID id)
		{
			ID = id;
		}

		public override string ToString()
		{
			return $"[{ID}->{ToStringImpl()}]";
		}

		protected abstract string ToStringImpl();
	}
}

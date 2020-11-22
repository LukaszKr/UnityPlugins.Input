using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class InputManager<ELayerID>: InputManager
	{
		public override Type EnumIDType { get { return typeof(ELayerID); } }

		public void PushReceiver(IInputReceiver receiver, ELayerID layerID)
		{
			PushReceiver(receiver, IDToInt(layerID));
		}

		protected virtual int IDToInt(ELayerID id)
		{
			return id.GetHashCode();
		}
	}
}

using System;

namespace ProceduralLevel.UnityPlugins.Input
{
	public class InputManager<ELayerID>: AInputManager
	{
		public override Type LayerIDType { get { return typeof(ELayerID); } }

		public void PushReceiver(IInputReceiver receiver, DetectorUpdater updater, ELayerID layerID)
		{
			PushReceiver(receiver, updater, IDToInt(layerID));
		}

		protected virtual int IDToInt(ELayerID id)
		{
			return id.GetHashCode();
		}
	}
}

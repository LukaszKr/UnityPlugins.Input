using System;
using System.Collections.Generic;

namespace UnityPlugins.Input.Unity
{
	public class InputReceiversManager
	{
		private int m_UpdateTick = 0;

		private readonly List<InputLayer> m_ActiveLayers = new List<InputLayer>();

		private readonly List<InputLayer> m_ToPush = new List<InputLayer>();
		private readonly List<IInputReceiver> m_ToPop = new List<IInputReceiver>();

		public IReadOnlyList<InputLayer> ActiveLayers => m_ActiveLayers;

		public void Update(float deltaTime)
		{
			m_UpdateTick++;

			for(int x = m_ToPop.Count - 1; x >= 0; --x)
			{
				IInputReceiver receiver = m_ToPop[x];
				int index = IndexOfReceiver(m_ActiveLayers, receiver);
				m_ActiveLayers.RemoveAt(index);
			}
			m_ToPop.Clear();

			int toPushCount = m_ToPush.Count;
			for(int x = 0; x < toPushCount; ++x)
			{
				InputLayer layer = m_ToPush[x];
				PushReceiverInternal(layer);
			}
			m_ToPush.Clear();

			bool canProceed = true;
			int count = m_ActiveLayers.Count;
			int lastValid = -1;
			for(int x = 0; x < count; ++x)
			{
				InputLayer layer = m_ActiveLayers[x];
				layer.IsActive = canProceed;
				if(canProceed)
				{
					lastValid = x;
					layer.Updater.Update(m_UpdateTick, deltaTime);
					layer.Receiver.UpdateInput();
				}
				if(layer.Definition.Block)
				{
					canProceed = false;
				}
			}
		}

		public void PushReceiver(IInputReceiver receiver, DetectorUpdater updater, InputLayerDefinition layerDefinition)
		{
			InputLayer newLayer = new InputLayer(receiver, updater, layerDefinition);
			m_ToPush.Add(newLayer);
		}

		private void PushReceiverInternal(InputLayer newLayer)
		{
			if(IndexOfReceiver(m_ActiveLayers, newLayer.Receiver) >= 0)
			{
				if(!m_ToPop.Remove(newLayer.Receiver))
				{
					throw new ArgumentException(string.Format("Receiver {0} is already active", newLayer.Receiver.ToString()));
				}
				return;
			}

			int count = m_ActiveLayers.Count;
			for(int x = 0; x < count; ++x)
			{
				InputLayer layer = m_ActiveLayers[x];
				if(layer.Definition.Priority < newLayer.Definition.Priority)
				{
					m_ActiveLayers.Insert(x, newLayer);
					return;
				}
			}
			m_ActiveLayers.Add(newLayer);
		}

		public bool PopReceiver(IInputReceiver receiver)
		{
			int index = IndexOfReceiver(m_ActiveLayers, receiver);
			if(index == -1)
			{
				index = IndexOfReceiver(m_ToPush, receiver);
				if(index != -1)
				{
					m_ToPush.RemoveAt(index);
					return true;
				}
				return false;
			}
			m_ToPop.Add(receiver);
			return true;
		}

		protected static int IndexOfReceiver(List<InputLayer> listToCheck, IInputReceiver receiver)
		{
			int count = listToCheck.Count;

			for(int x = 0; x != count; ++x)
			{
				InputLayer layer = listToCheck[x];
				if(layer.Receiver == receiver)
				{
					return x;
				}
			}
			return -1;
		}
	}
}

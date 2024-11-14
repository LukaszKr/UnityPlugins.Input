using NUnit.Framework;

namespace UnityPlugins.Input.Unity.Receivers
{
	[Category(InputTestsConsts.CATEGORY_ASSEMBLY)]
	public class InputReceiversManagerTests
	{
		private InputReceiversManager m_Manager;

		[SetUp]
		public void Setup()
		{
			m_Manager = new InputReceiversManager();
		}

		[Test]
		public void PushReceiver_BecomesActiveAfterUpdate()
		{
			TestInputReceiver receiver = new TestInputReceiver();
			m_Manager.PushReceiver(receiver, receiver.Updater, new InputLayerDefinition());

			Assert.AreEqual(0, m_Manager.ActiveLayers.Count);
			m_Manager.Update(0f);
			Assert.AreEqual(1, m_Manager.ActiveLayers.Count);
			Assert.AreEqual(1, receiver.UpdateCount, "Should update after it becomes active.");
			Assert.AreEqual(receiver, m_Manager.ActiveLayers[0].Receiver);
		}

		[Test]
		public void PopReceiver_BecomesInactiveAfterUpdate()
		{
			TestInputReceiver receiver = new TestInputReceiver();
			m_Manager.PushReceiver(receiver, receiver.Updater, new InputLayerDefinition());

			m_Manager.Update(0f);
			Assert.AreEqual(1, m_Manager.ActiveLayers.Count);
			Assert.AreEqual(1, receiver.UpdateCount);

			m_Manager.PopReceiver(receiver);
			Assert.AreEqual(1, m_Manager.ActiveLayers.Count);
			
			m_Manager.Update(0f);
			Assert.AreEqual(0, m_Manager.ActiveLayers.Count);
		}

		[Test, Description("Higher priority layers will block lower priority. Push order doesn't matter if priorities are different.")]
		public void BlockingLayers_HigherPriority_PushedLast()
		{
			TestInputReceiver receiverA = new TestInputReceiver();
			TestInputReceiver receiverB = new TestInputReceiver();
			m_Manager.PushReceiver(receiverA, receiverA.Updater, new InputLayerDefinition(string.Empty, 1, false));
			m_Manager.PushReceiver(receiverB, receiverB.Updater, new InputLayerDefinition(string.Empty, 2, true));

			m_Manager.Update(0f);

			Assert.AreEqual(0, receiverA.UpdateCount);
			Assert.AreEqual(1, receiverB.UpdateCount);
			Assert.AreEqual(2, m_Manager.ActiveLayers.Count);
		}

		[Test, Description("Higher priority layers will block lower priority. Push order doesn't matter if priorities are different.")]
		public void BlockingLayers_HigherPriority_PushedFirst()
		{
			TestInputReceiver receiverA = new TestInputReceiver();
			TestInputReceiver receiverB = new TestInputReceiver();
			m_Manager.PushReceiver(receiverB, receiverB.Updater, new InputLayerDefinition(string.Empty, 2, true));
			m_Manager.PushReceiver(receiverA, receiverA.Updater, new InputLayerDefinition(string.Empty, 1, false));

			m_Manager.Update(0f);

			Assert.AreEqual(0, receiverA.UpdateCount);
			Assert.AreEqual(1, receiverB.UpdateCount);
			Assert.AreEqual(2, m_Manager.ActiveLayers.Count);
		}

		[Test, Description("Higher priority layers will block lower priority. Push order matter if priorities are the same.")]
		public void BlockingLayers_SamePriority_PushedFirst()
		{
			TestInputReceiver receiverA = new TestInputReceiver();
			TestInputReceiver receiverB = new TestInputReceiver();
			m_Manager.PushReceiver(receiverB, receiverB.Updater, new InputLayerDefinition(string.Empty, 2, true));
			m_Manager.PushReceiver(receiverA, receiverA.Updater, new InputLayerDefinition(string.Empty, 2, false));

			m_Manager.Update(0f);

			Assert.AreEqual(0, receiverA.UpdateCount);
			Assert.AreEqual(1, receiverB.UpdateCount);
			Assert.AreEqual(2, m_Manager.ActiveLayers.Count);
		}

		[Test, Description("Higher priority layers will block lower priority. Push order matter if priorities are the same.")]
		public void BlockingLayers_SamePriority_PushedLast()
		{
			TestInputReceiver receiverA = new TestInputReceiver();
			TestInputReceiver receiverB = new TestInputReceiver();
			TestInputReceiver receiverC = new TestInputReceiver();
			m_Manager.PushReceiver(receiverC, receiverA.Updater, new InputLayerDefinition(string.Empty, 1, false));
			m_Manager.PushReceiver(receiverA, receiverA.Updater, new InputLayerDefinition(string.Empty, 2, false));
			m_Manager.PushReceiver(receiverB, receiverB.Updater, new InputLayerDefinition(string.Empty, 2, true));

			m_Manager.Update(0f);

			Assert.AreEqual(1, receiverA.UpdateCount);
			Assert.AreEqual(1, receiverB.UpdateCount);
			Assert.AreEqual(0, receiverC.UpdateCount);
			Assert.AreEqual(3, m_Manager.ActiveLayers.Count);
		}

		[Test]
		public void BlockingLayers_RemoveBlocker()
		{
			TestInputReceiver receiverA = new TestInputReceiver();
			TestInputReceiver receiverB = new TestInputReceiver();
			m_Manager.PushReceiver(receiverA, receiverA.Updater, new InputLayerDefinition(string.Empty, 1, false));
			m_Manager.PushReceiver(receiverB, receiverB.Updater, new InputLayerDefinition(string.Empty, 2, true));

			m_Manager.Update(0f);
			Assert.AreEqual(0, receiverA.UpdateCount);
			Assert.AreEqual(1, receiverB.UpdateCount);
			Assert.AreEqual(2, m_Manager.ActiveLayers.Count);

			m_Manager.PopReceiver(receiverB);
			m_Manager.Update(0f);
			Assert.AreEqual(1, receiverA.UpdateCount);
			Assert.AreEqual(1, receiverB.UpdateCount);
			Assert.AreEqual(1, m_Manager.ActiveLayers.Count);
		}
	}
}

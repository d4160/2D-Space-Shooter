namespace GameFramework
{
#if PHOTON_UNITY_NETWORKING
	using Photon.Pun;
#endif
	using d4160.Networking;

	public class ChatController : DefaultChatController
	{
        public override void Connect()
        {
            base.Connect();

            if (m_chatProvider != null)
            {
				if (NotificationPrefabsManager.Instanced)
				    NotificationPrefabsManager.Instance.InstancedMain?.Notify("Connecting to chat...", 1.5f);
            }
        }

        protected override void OnConnected()
        {
            base.OnConnected();

            if (NotificationPrefabsManager.Instanced)
				NotificationPrefabsManager.Instance.InstancedMain?.Notify("Chat connection success.", 1.5f);
		}

        public override void CreateChatProvider()
		{
				switch(m_chatProviderType)
				{

						case ChatProviderType.PhotonUnityNetworking:
#if PHOTON_UNITY_NETWORKING
								m_chatProvider = new PUNChatProvider(
									PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat);
#endif
						break;
				}
		}
	}
}
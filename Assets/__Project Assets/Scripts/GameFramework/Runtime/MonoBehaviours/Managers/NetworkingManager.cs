namespace GameFramework
{
    using UnityEngine;
    using UnityExtensions;
    using d4160.Networking;

    public class NetworkingManager : NetworkingManagerBase
    {
        [SerializeField, InspectInline] protected NetworkingController m_networkingController;
        [SerializeField, InspectInline] protected ChatController m_chat;

        public override NetworkingControllerBase NetworkingController => m_networkingController;
        public override ChatControllerBase Chat => m_chat;
    }
}
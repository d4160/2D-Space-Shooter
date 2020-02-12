namespace GameFramework
{
    using UnityEngine;

    public class NetworkingLoader : MonoBehaviour
    {
        [SerializeField] protected bool m_alsoConnectToChat;

        public void ConnectAfterLoginAll()
        {
            //PUN2Launcher.Instance.SetAuthenticatingNotification();

            // Assuming is the same Authenticator for all
            DataManager.Instance.GameData.Authenticator.Login(() => {
                Connect();
            });
        }

        public void Connect()
        {
            NetworkingManager.Instance.NetworkingController.Connect();
            if (m_alsoConnectToChat)
                NetworkingManager.Instance.Chat.Connect();
        }
    }
}
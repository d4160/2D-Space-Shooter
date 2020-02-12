namespace GameFramework
{
    using UnityEngine;

    public class ChatLoader : MonoBehaviour
    {
        public void ConnectAfterAuthenticate()
        {
            var chat = NetworkingManager.Instance.Chat;
            chat.Authenticate();

            Connect();
        }

        public void Connect()
        {
            NetworkingManager.Instance.Chat.Connect();
        }

        public void Disconnect()
        {
            NetworkingManager.Instance.Chat.Disconnect();
        }

        public virtual void AppendHelpToCurrentChannel()
		{
			NetworkingManager.Instance.Chat.AppendHelpToCurrentChannel();
		}

    }
}
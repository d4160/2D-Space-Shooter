namespace d4160.Networking
{
    using Photon.Chat;
    using UnityEngine;
    using System;
    using ExitGames.Client.Photon;

    public class PUNChatProvider : IChatProvider, IChatClientListener
    {
#if NAUGHTY_ATTRIBUTES
        [Popup("Any", "EU", "US", "ASIA")]
#endif
		[SerializeField] protected string m_chatRegion = "US";

        protected ChatClient m_chatClient;
        protected string m_appIdChat;
        protected string m_photonCustomAuthenticationToken;
        protected AuthenticationValues m_authValues;

        public string Username => m_chatClient?.AuthValues?.UserId;
        public string PhotonCustomAuthenticationToken
        {
            get => m_photonCustomAuthenticationToken;
            set => m_photonCustomAuthenticationToken = value;
        }
        public bool UseCustomAuth { get; set; }

        public Action<string[]> OnSubscribedAction { get; set; }
        public Action<string[]> OnUnsubscribedAction { get; set; }
        public Action OnConnectedAction { get; set; }
        public Action OnDisconnectedAction { get; set; }
        public Action<string, string[], object[]> OnGetMessagesAction { get; set; }
        public Action<string, object, string> OnPrivateMessageAction { get; set; }
        public Action<string, int, bool, object> OnStatusUpdateAction { get; set; }
        public Action<string, string> OnUserSubscribedAction { get; set; }
        public Action<string, string> OnUserUnsubscribedAction { get; set; }
        public Action<string> OnChatStateChangedAction { get; set; }

        public PUNChatProvider(string appIdChat)
        {
            m_appIdChat = appIdChat;
        }

        public virtual void Authenticate(string displayUsername, string customUsername)
        {
            if (UseCustomAuth)
            {
                m_authValues = new Photon.Chat.AuthenticationValues
                {
                    AuthType = Photon.Chat.CustomAuthenticationType.Custom,
                };
                m_authValues.AddAuthParameter("username", customUsername);    // expected by PlayFab custom auth service
                //We add "token" parameter. PlayFab expects it to contain Photon Authentication Token issues to your during previous step.
                m_authValues.AddAuthParameter("token", m_photonCustomAuthenticationToken);
                m_authValues.UserId = displayUsername;
            }
            else
            {
                m_authValues = new Photon.Chat.AuthenticationValues
                {
                    UserId = displayUsername
                };
            }
        }

        public virtual void Connect(string version)
		{
			m_chatClient = new ChatClient(this);
#if !UNITY_WEBGL
            m_chatClient.UseBackgroundWorkerForSending = true;
#endif
			if(m_chatRegion != "Any")
				m_chatClient.ChatRegion = m_chatRegion;

			m_chatClient.Connect(m_appIdChat, version, m_authValues);
		}

        public virtual void Disconnect()
        {
            if(m_chatClient != null)
            {
                m_chatClient.Disconnect();
            }
        }

        public virtual void UpdateService()
        {
            if (m_chatClient != null)
                m_chatClient.Service();
        }

        public void OnConnected()
        {
            OnConnectedAction?.Invoke();
        }

        public void OnDisconnected()
        {
            OnDisconnectedAction?.Invoke();
        }

        public void SendPrivateMessage(string target, object message, bool forwardAsWebhook = false)
        {
            m_chatClient.SendPrivateMessage(target, message, forwardAsWebhook);
        }

        public bool ContainsPrivateChannel(string channel)
        {
            return m_chatClient.PrivateChannels.ContainsKey(channel);
        }

        public void RemovePrivateChannel(string channel)
        {
            m_chatClient.PrivateChannels.Remove(channel);

            OnUnsubscribedAction?.Invoke(new string[]{ channel });
        }

        public void ClearChannelMessages(string channelName, bool isPrivate)
        {
            ChatChannel channel;
            if (m_chatClient.TryGetChannel(channelName, isPrivate, out channel))
            {
                channel.ClearMessages();
            }
        }

        public bool GetChannelMessages(string channelName, out string messages)
        {
            ChatChannel channel = null;
			bool found = m_chatClient.TryGetChannel(channelName, out channel);
			if (!found)
			{
                messages = string.Empty;
				return false;
			}

            messages = channel.ToStringMessages();
            return true;
        }

        public bool AddMessageToChannel(string channelName, string message)
        {
            ChatChannel channel = null;
            bool found = m_chatClient.TryGetChannel(channelName, out channel);
            if (!found)
            {
                return false;
            }

            if (channel != null)
            {
                channel.Add("Bot", message,0); //TODO: how to use msgID?

                return true;
            }

            return false;
        }

        public void SetOnlineStatus(int newState, object message = null)
        {
            if (message == null)
            {
                m_chatClient.SetOnlineStatus(newState);
            }
            else
            {
                m_chatClient.SetOnlineStatus(newState, message);
            }
        }

        public void Subscribe(string[] channels, int messagesFromHistory = 1)
        {
            m_chatClient.Subscribe(channels, messagesFromHistory);
        }

        public void Unsubscribe(string[] channels)
        {
            m_chatClient.Unsubscribe(channels);
        }

        public void AddFriends(string[] friends)
        {
            m_chatClient.AddFriends(friends);
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            if (level == DebugLevel.ERROR)
            {
                Debug.LogError(message);
            }
            else if (level == DebugLevel.WARNING)
            {
                Debug.LogWarning(message);
            }
            else
            {
                Debug.Log(message);
            }
        }

        public void OnChatStateChange(ChatState state)
        {
            OnChatStateChangedAction?.Invoke(state.ToString());
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            OnGetMessagesAction?.Invoke(channelName, senders, messages);
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            OnPrivateMessageAction?.Invoke(sender, message, channelName);
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            OnSubscribedAction?.Invoke(channels);
        }

        public void OnUnsubscribed(string[] channels)
        {
            OnUnsubscribedAction?.Invoke(channels);
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            OnStatusUpdateAction?.Invoke(user, status, gotMessage, message);
        }

        public void OnUserSubscribed(string channel, string user)
        {
            OnUserSubscribedAction?.Invoke(channel, user);
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            OnUserUnsubscribedAction?.Invoke(channel, user);
        }

        public void PublishMessage(string channel, object message, bool forwardAsWebhook = false)
        {
            m_chatClient.PublishMessage(channel, message, forwardAsWebhook);
        }
    }
}
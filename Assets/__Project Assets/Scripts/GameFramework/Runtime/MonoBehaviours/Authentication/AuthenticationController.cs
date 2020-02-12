namespace GameFramework
{
    using System;
    using UnityEngine;
    using TMPro;

    public class AuthenticationController : MonoBehaviour
    {
        public TextMeshProUGUI UsernameInfoText;
        public TextMeshProUGUI UsernameInfoTextMultiplayer;
        public bool AuthenticateAtStart;
        public bool ConnectToChatWhenAuthenticated;

        private void Start()
        {
            if (AuthenticateAtStart)
            {
                if (DataManager.Instance.GameData.Authenticator.CanAuthenticate)
                    LoginAll();
                else
                    SetUsernameUI();
            }
            else
            {
                SetUsernameUI();
            }
        }

        private void SetUsernameUI()
        {
            if (UsernameInfoText)
                UsernameInfoText.text = $"Logged as {DataManager.Instance.GameData.Authenticator.Username}";

            if (UsernameInfoTextMultiplayer)
                UsernameInfoTextMultiplayer.text = DataManager.Instance.GameData.Authenticator.Username;
        }

        public void LoginAll()
        {
            LoginAll(() =>
            {
                SetUsernameUI();

                if (ConnectToChatWhenAuthenticated)
                    NetworkingManager.Instance.Chat.Connect();
            });
        }

        public void LoginAll(Action onCompleted, Action onFailed = null)
        {
            // Since all share the same authenticator
            DataManager.Instance.GameData.Authenticator.Login(onCompleted, onFailed);
        }

        public void LogoutAll()
        {
            // Since all share the same authenticator
            DataManager.Instance.GameData.Authenticator.Logout();
        }

        public void SetOverrideAuthentication(bool setValue)
        {
            // Since all share the same authenticator
            DataManager.Instance.GameData.Authenticator.AllowOverrideAuthentication = setValue;
        }
    }
}
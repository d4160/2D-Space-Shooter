namespace GameFramework
{
    using DataPersistence;
    using System;
    using d4160.DataPersistence;
    using UnityEngine;
    using PlayFab.ClientModels;
    using Photon.Pun;
    using PlayFab;
    using d4160.Networking;
    using d4160.UI.Notification;

    public class AuthenticatorController : DefaultAuthenticatorController
    {
        public override void Login(Action onCompleted, Action onFailed = null)
        {
            NotificationPrefabsManagerBase.Instance.InstancedMain.Notify("Authenticating...", 1.5f);
            //ProgressPrefabsManagerBase.Instance.InstancedMain.StartLoop();

            base.Login(onCompleted, onFailed);
        }

        protected override IAuthenticator CreateAuthenticator(Action onCompleted = null, Action onFailed = null)
        {
            IAuthenticator authenticator = null;
            string customId = m_useDeviceUniqueIdentifier ? PlayFab.PlayFabSettings.DeviceUniqueIdentifier : m_username;
            if (m_authenticationType == AuthenticationType.Remote)
            {
                authenticator = new CustomIdAuthenticator
                (
                    customId,
                    (result) =>
                    {
                        UpdateTitleDisplayName(() =>
                        {
                            if (m_multiplayerAuthentication)
                                RequestPhotonToken(result, onCompleted);
                            else
                            {
                                NotificationPrefabsManagerBase.Instance.InstancedMain.Notify("Authentication complete.", 1.5f);

                                onCompleted?.Invoke();
                            }
                            
                        }, () =>
                        {
                            NotificationPrefabsManagerBase.Instance.InstancedMain.Notify("That name is not available.", 1.5f);
                        });
                    },
                    (error) =>
                    {
                        NotificationPrefabsManagerBase.Instance.InstancedMain.Notify("Something went wrong, check your internet connection.", 1.5f);

                        onFailed?.Invoke();
                        Debug.LogError($"Error with PlayFab CustomIdAuthenticator: {error.GenerateErrorReport()}");
                    }
                );
            }
            else
            {
                authenticator = new LocalAuthenticator
                (
                    m_username,
                    () => {
                        onCompleted?.Invoke();
                        Debug.Log("Login or logout locally success!");
                    }, () => {
                        onFailed?.Invoke();
                        Debug.LogError("Something went wrong with local login or logout.");
                    }
                );
            }

            return authenticator;
        }

        private void UpdateTitleDisplayName(Action onCompleted = null, Action onFailed = null)
        {
            PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest(){
                DisplayName = m_username
            }, (result) => {
                Debug.Log($"DisplayName updated: {result.DisplayName}.");

                onCompleted?.Invoke();
            }, (error) => {
                Debug.LogError($"Updating display name error: {error.GenerateErrorReport()}");

                onFailed?.Invoke();
            }
            );
        }

        private void RequestPhotonToken(LoginResult obj, Action onCompleted = null) {
            LogMessage("PlayFab authenticated. Requesting photon token...");
            NotificationPrefabsManagerBase.Instance.InstancedMain.Notify("Authenticating to Photon Unity Networking.", 1.5f);

            PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
            {
                PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
            }, (result) => {
                AuthenticateWithPhoton(result);

                NotificationPrefabsManagerBase.Instance.InstancedMain.Notify("Authentication complete.", 1.5f);

                onCompleted?.Invoke();
            }, OnPlayFabError);
        }

        private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
        {
            LogMessage("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

            //We set AuthType to custom, meaning we bring our own, PlayFab authentication procedure.
            var photonAuth = new Photon.Realtime.AuthenticationValues { AuthType = Photon.Realtime.CustomAuthenticationType.Custom };
            //We add "username" parameter. Do not let it confuse you: PlayFab is expecting this parameter to contain player PlayFab ID (!) and not username.
            photonAuth.AddAuthParameter("username", m_authenticator.Id);    // expected by PlayFab custom auth service

            //We add "token" parameter. PlayFab expects it to contain Photon Authentication Token issues to your during previous step.
            photonAuth.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);

            if (m_chatAuthentication)
            {
                var chatController = NetworkingManager.Instance.Chat;
                if (!chatController) return;

                // If using Photon Auth, only have PUNChatProvider
                var provider = chatController.ChatProvider;
                if (provider != null && provider is PUNChatProvider punChatProvider)
                {
                    punChatProvider.PhotonCustomAuthenticationToken = obj.PhotonCustomAuthenticationToken;
                    punChatProvider.Authenticate(m_username, m_authenticator.Id);
                }
            }

            photonAuth.UserId = m_username;

            // We finally tell Photon to use this authentication parameters throughout the entire application.
            PhotonNetwork.AuthValues = photonAuth;
        }

        private void OnPlayFabError(PlayFabError obj) {
            LogMessage(obj.GenerateErrorReport());
        }

        private void LogMessage(string message) {
            Debug.Log("PlayFab + Photon: " + message);
        }
    }
}
namespace GameFramework
{
    using d4160.GameFramework;
    using System.Collections.Generic;
#if PLAYFAB
    using PlayFab;
    using PlayFab.ClientModels;
#endif

    [System.Serializable]
    public class AppStatsSettingsSerializableData : DefaultAppStatsSettingsSerializableData
    {
        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public AppStatsSettingsSerializableData() : base()
        {
        }

        protected override void LoadForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
#if PLAYFAB
            PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                Keys = null
            }, result => {
                if (result.Data != null && result.Data.ContainsKey(nameof(fps)))
                {
                    fps = bool.Parse(result.Data[nameof(fps)].Value);
                    ram = bool.Parse(result.Data[nameof(ram)].Value);
                    audio = bool.Parse(result.Data[nameof(audio)].Value);
                    advancedInfo = bool.Parse(result.Data[nameof(advancedInfo)].Value);
                }

                onCompleted?.Invoke();
            }, null);
#endif
        }

        protected override void SaveForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
#if PLAYFAB
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                Data = new Dictionary<string, string>() {
                    { nameof(fps), fps.ToString() },
                    { nameof(ram), ram.ToString() },
                    { nameof(audio), audio.ToString() },
                    { nameof(advancedInfo), advancedInfo.ToString() }
                }
            }, (result) => onCompleted?.Invoke(), null);
#endif
        }
    }
}
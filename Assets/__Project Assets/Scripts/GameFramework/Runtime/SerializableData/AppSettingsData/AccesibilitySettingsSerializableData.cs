namespace GameFramework
{
    using d4160.GameFramework;
    using System.Collections.Generic;
#if PLAYFAB
    using PlayFab;
    using PlayFab.ClientModels;
#endif

    [System.Serializable]
    public class AccesibilitySettingsSerializableData : DefaultAccesibilitySettingsSerializableData
    {
        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public AccesibilitySettingsSerializableData() : base()
        {
        }

        protected override void LoadForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
#if PLAYFAB
            PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                Keys = null
            }, result => {
                if (result.Data != null && result.Data.ContainsKey(nameof(uiScale)))
                {
                    uiScale = float.Parse(result.Data[nameof(uiScale)].Value);
                    subtitles = bool.Parse(result.Data[nameof(subtitles)].Value);
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
                    { nameof(uiScale), uiScale.ToString() },
                    { nameof(subtitles), subtitles.ToString() },
                }
            }, (result) => onCompleted?.Invoke(), null);
#endif
        }
    }
}
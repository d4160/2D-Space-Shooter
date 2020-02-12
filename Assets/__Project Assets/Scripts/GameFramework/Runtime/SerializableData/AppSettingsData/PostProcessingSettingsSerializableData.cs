namespace GameFramework
{
    using d4160.GameFramework;
    using System.Collections.Generic;
#if PLAYFAB
    using PlayFab;
    using PlayFab.ClientModels;
#endif

    [System.Serializable]
    public class PostProcessingSettingsSerializableData : DefaultPostProcessingSettingsSerializableData
    {
        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public PostProcessingSettingsSerializableData() : base()
        {
        }

        protected override void LoadForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
#if PLAYFAB
            PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                Keys = null
            }, result => {
                if (result.Data != null && result.Data.ContainsKey(nameof(bloom)))
                {
                    bloom = bool.Parse(result.Data[nameof(bloom)].Value);
                    colorGrading = bool.Parse(result.Data[nameof(colorGrading)].Value);
                    vignette = bool.Parse(result.Data[nameof(vignette)].Value);
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
                    { nameof(bloom), bloom.ToString() },
                    { nameof(colorGrading), colorGrading.ToString() },
                    { nameof(vignette), vignette.ToString() },
                }
            }, (result) => onCompleted?.Invoke(), null);
#endif
        }
    }
}
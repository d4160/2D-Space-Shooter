namespace GameFramework
{
    using d4160.GameFramework;
    using System.Collections.Generic;
#if PLAYFAB
    using PlayFab;
    using PlayFab.ClientModels;
#endif
    using UnityEngine;

    [System.Serializable]
    public class LocalizationSettingsSerializableData : DefaultLocalizationSettingsSerializableData
    {
        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public LocalizationSettingsSerializableData() : base()
        {
        }

        protected override void LoadForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
#if PLAYFAB
            PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                Keys = null
            }, result => {
                if (result.Data != null && result.Data.ContainsKey(nameof(textLanguage)))
                {
                    textLanguage = (SystemLanguage)System.Enum.Parse(typeof(SystemLanguage),result.Data[nameof(textLanguage)].Value);
                    voiceLanguage = (SystemLanguage)System.Enum.Parse(typeof(SystemLanguage), result.Data[nameof(voiceLanguage)].Value);
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
                    { nameof(textLanguage), textLanguage.ToString() },
                    { nameof(voiceLanguage), voiceLanguage.ToString() },
                }
            }, (result) => onCompleted?.Invoke(), null);
#endif
        }
    }
}
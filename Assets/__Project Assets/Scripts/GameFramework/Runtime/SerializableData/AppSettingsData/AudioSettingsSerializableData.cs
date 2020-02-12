namespace GameFramework
{
    using d4160.GameFramework;
#if PLAYFAB
    using PlayFab;
    using PlayFab.ClientModels;
#endif
    using System.Collections.Generic;

    [System.Serializable]
    public class AudioSettingsSerializableData : DefaultAudioSettingsSerializableData
    {
        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public AudioSettingsSerializableData() : base()
        {
        }

        protected override void LoadForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
#if PLAYFAB
            PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                Keys = null
            }, result => {
                if (result.Data != null && result.Data.ContainsKey(nameof(music)))
                {
                    music = bool.Parse(result.Data[nameof(music)].Value);
                    musicVolume = float.Parse(result.Data[nameof(musicVolume)].Value);
                    sfxs = bool.Parse(result.Data[nameof(sfxs)].Value);
                    sfxsVolume = float.Parse(result.Data[nameof(sfxsVolume)].Value);
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
                    { nameof(music), music.ToString() },
                    { nameof(musicVolume), musicVolume.ToString() },
                    { nameof(sfxs), sfxs.ToString() },
                    { nameof(sfxsVolume), sfxsVolume.ToString() }
                }
            }, (result) => onCompleted?.Invoke(), null);
#endif
        }
    }
}
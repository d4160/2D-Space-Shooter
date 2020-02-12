namespace GameFramework
{
    using d4160.GameFramework;
    using System.Collections.Generic;
    using d4160.DataPersistence;
#if PLAYFAB
    using PlayFab;
    using PlayFab.ClientModels;
#endif
    using UnityEngine;

    [System.Serializable]
    public class GraphicsSettingsSerializableData : DefaultGraphicsSettingsSerializableData, IStorageHelper
    {
        public GraphicsSettingsSerializableData() : base()
        {

        }

        protected override void LoadForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
#if PLAYFAB
            PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                Keys = null
            }, result => {
                if (result.Data != null && result.Data.ContainsKey(nameof(resolution)))
                {
                    resolution = int.Parse(result.Data[nameof(resolution)].Value);
                    fullScreenMode = (FullScreenMode)System.Enum.Parse(typeof(FullScreenMode), result.Data[nameof(fullScreenMode)].Value);
                    qualityLevel = int.Parse(result.Data[nameof(qualityLevel)].Value);
                    vSyncCount = int.Parse(result.Data[nameof(vSyncCount)].Value);
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
                    { nameof(resolution), resolution.ToString() },
                    { nameof(fullScreenMode), fullScreenMode.ToString() },
                    { nameof(qualityLevel), qualityLevel.ToString() },
                    { nameof(vSyncCount), vSyncCount.ToString() }
                }
            }, (result) => onCompleted?.Invoke(), null);
#endif
        }
    }
}
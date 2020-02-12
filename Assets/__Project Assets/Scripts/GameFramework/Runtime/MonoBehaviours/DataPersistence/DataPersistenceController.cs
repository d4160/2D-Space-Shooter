namespace GameFramework
{
    using UnityEngine;

    public class DataPersistenceController : MonoBehaviour
    {
        public void SaveGameData()
        {
            DataManager.Instance.GameData.DataLoader.Save();
        }

        public void LoadGameData()
        {
            DataManager.Instance.GameData.DataLoader.Load();
        }

        public void SaveAppSettingsData()
        {
            DataManager.Instance.AppSettingsData.DataLoader.Save();
        }

        public void LoadAppSettingsData()
        {
            DataManager.Instance.AppSettingsData.DataLoader.Load();
        }

        public void SavePlayerData()
        {
            DataManager.Instance.PlayerData.DataLoader.Save();
        }

        public void LoadPlayerData()
        {
            DataManager.Instance.PlayerData.DataLoader.Load();
        }

        public void SaveGameFoundationData()
        {
            DataManager.Instance.GameFoundationData.DataLoader.Save();
        }

        public void LoadGameFoundationData()
        {
            DataManager.Instance.GameFoundationData.DataLoader.Load();
        }
    }
}
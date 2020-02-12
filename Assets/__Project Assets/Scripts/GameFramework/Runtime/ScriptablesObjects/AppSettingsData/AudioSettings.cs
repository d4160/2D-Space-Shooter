namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New AudioSettings_SO.asset", menuName = "Game Framework/App Settings Data/Audio")]
    public class AudioSettings : DefaultAudioSettings<AudioSettingsSerializableData>
    {
        public override AudioSettingsSerializableData Get()
        {
            var data = new AudioSettingsSerializableData();
            data.music = m_music;
            data.musicVolume = m_musicVolume;
            data.sfxs = m_sfxs;
            data.sfxsVolume = m_sfxsVolume;

            return data;
        }

        public override void Set(AudioSettingsSerializableData data)
        {
            Music = data.music;
            MusicVolume = data.musicVolume;
            Sfxs = data.sfxs;
            SfxsVolume = data.sfxsVolume;
        }
    }
}
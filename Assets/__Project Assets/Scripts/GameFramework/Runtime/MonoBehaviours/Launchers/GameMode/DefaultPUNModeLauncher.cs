#if PHOTON_UNITY_NETWORKING
using UnityEngine;
using d4160.GameFramework;

namespace GameFramework
{
    public class DefaultPUNModeLauncher : DefaultPUNPlayLauncher
    {
        [SerializeField] protected bool m_startPlayingAutomatically;

        //[DeConditional("m_startPlayingAutomatically", true, ConditionalBehaviour.Hide)]
        [SerializeField] protected float m_delayToStartPlaying;

        protected float m_playingTime;

        public override void SetReadyToPlay()
        {
            base.SetReadyToPlay();

            m_playingTime = 0f;

            if (m_startPlayingAutomatically)
            {
                if (m_delayToStartPlaying > 0)
                {
                    Invoke("Play", m_delayToStartPlaying);
                }
                else
                {
                    Play();
                }
            }
        }
    }
}
#endif
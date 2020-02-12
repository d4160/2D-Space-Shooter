using UnityEngine;
using d4160.GameFramework;

namespace GameFramework
{
    public class DefaultModeLauncher : DefaultPlayLauncher
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
                Invoke("Play", m_delayToStartPlaying);
        }
    }
}
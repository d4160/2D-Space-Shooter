namespace GameFramework
{
    using d4160.GameFramework;
    using System.Collections.Generic;
#if PLAYFAB
    using PlayFab;
    using PlayFab.ClientModels;
#endif

    [System.Serializable]
    public class PlayersSerializableData : DefaultPlayersSerializableData
    {
        public PlayersSerializableData(DefaultPlayer[] elements) : base(elements)
        {
        }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public PlayersSerializableData() : base()
        {
        }

        protected override void LoadForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
#if PLAYFAB

#endif
        }

        protected override void SaveForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
#if PLAYFAB

#endif
        }
    }
}
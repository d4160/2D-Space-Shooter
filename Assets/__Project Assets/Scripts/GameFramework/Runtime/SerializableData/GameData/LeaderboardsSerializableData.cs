namespace GameFramework
{
    using d4160.GameFramework;
    using System.Collections.Generic;
#if PLAYFAB
    using PlayFab;
    using PlayFab.ClientModels;
#endif

    [System.Serializable]
    public class LeaderboardsSerializableData : DefaultLeaderboardsSerializableData
    {
        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public LeaderboardsSerializableData() : base()
        {
        }

        public LeaderboardsSerializableData(DefaultLeaderboard[] elements) : base(elements)
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
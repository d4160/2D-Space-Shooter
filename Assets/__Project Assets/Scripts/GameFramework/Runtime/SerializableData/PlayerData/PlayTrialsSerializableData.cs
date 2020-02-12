namespace GameFramework
{
    using d4160.GameFramework;
#if PLAYFAB
#endif

    [System.Serializable]
    public class PlayTrialsSerializableData : DefaultPlayTrialsSerializableData
    {
        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public PlayTrialsSerializableData() : base()
        {
        }

        public PlayTrialsSerializableData(DefaultPlayTrial[] elements) : base(elements)
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
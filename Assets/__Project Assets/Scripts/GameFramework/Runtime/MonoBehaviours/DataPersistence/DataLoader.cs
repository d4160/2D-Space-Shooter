namespace GameFramework
{
    using d4160.GameFramework;
    using d4160.DataPersistence;
    using UnityEngine;
#if ODIN_SERIALIZER
    using OdinSerializer;
    using DataSerializerType = d4160.DataPersistence.DataSerializerType;
#endif
    using UnityEngine.GameFoundation.DataPersistence;
#if NAUGHTY_ATTRIBUTES
    using NaughtyAttributes;
#endif

    public class DataLoader : DefaultDataLoader
    {
#if ODIN_SERIALIZER
#if NAUGHTY_ATTRIBUTES
        // Odin only
        [ShowIf(ConditionOperator.And, "IsDataPersistenceLocal", "IsSerializerOdin")]
#endif
        [SerializeField] protected DataFormat m_dataFormat = DataFormat.JSON;

        #region Editor Only
#if UNITY_EDITOR
        private bool IsSerializerOdin => m_serializerType == DataSerializerType.Odin;
#endif
        #endregion
#endif
        public override void CreateDataPersistence()
        {
            base.CreateDataPersistence();

            //Debug.Log($"Data persistence created: {DataPersistenceString}");
        }

        protected override IDataPersistence CreateDataPersistenceForRemote(IDataSerializer serializer, IStorageHelper storageHelper)
        {
            IDataPersistence dataPersistence = null;

            if (m_persistenceTarget == DataPersistenceTarget.GameFoundation)
            {
#if PLAYFAB
                var dp  = new PlayFabPersistenceWithIdentifier(
                    serializer, m_encrypted,
                    m_authenticator.AuthenticationId, storageHelper);
                dp.SetIdentifier(Identifier);
                dataPersistence = dp;
#endif
            }
            else
            {
#if PLAYFAB
                dataPersistence = new DefaultPlayFabPersistence(
                    serializer, m_encrypted,
                    m_authenticator.AuthenticationId, storageHelper
                );
#endif
            }

            return dataPersistence;
        }

        protected override IStorageHelper CreateStorageHelperForRemote(IDataSerializationAdapter serializationAdapter)
        {
            IStorageHelper storageHelper = null;

            if (!m_remoteStorageInOneEntry)
            {
                storageHelper = serializationAdapter.GetSerializableData() as IStorageHelper;
                if(storageHelper != null)
                    storageHelper.StorageHelperType = StorageHelperType.PlayFab;
            }

            return storageHelper;
        }

        protected override IDataSerializer CreateDataSerializerForOdin()
        {
            IDataSerializer dataSerializer = null;

#if ODIN_SERIALIZER
            dataSerializer = new OdinDataSerializer(m_dataFormat);
#endif
            return dataSerializer;
        }

        public override void Initialize()
        {
            base.Initialize();

            //Debug.Log("Loader initialized.");
        }

        public override void Deinitialize()
        {
            base.Deinitialize();

            //Debug.Log("Loader uninitialized.");
        }

        public override void Load()
        {
            m_dataSerializationAdapter.Load(m_dataPersistence,
            () => Debug.Log("Load Completed"),
            () => Debug.Log("Load Failed"));
        }

        public override void Save()
        {
            m_dataSerializationAdapter.Save(m_dataPersistence,
            () => Debug.Log("Save Completed"),
            () => Debug.Log("Save Failed"));
        }

        protected override IDataSerializationAdapter CreateSerializationAdapterForAppSettings(DataSerializationAdapterType adapterType)
        {
            return new AppSettingsDataSerializationAdapter(m_adapterType);
        }

        protected override IDataSerializationAdapter CreateSerializationAdapterForGame(DataSerializationAdapterType adapterType)
        {
            return new GameDataSerializationAdapter(m_adapterType);
        }

        protected override IDataSerializationAdapter CreateSerializationAdapterForPlayer(DataSerializationAdapterType adapterType)
        {
            return new PlayerDataSerializationAdapter(m_adapterType);
        }
    }
}
namespace GameFramework
{
    using d4160.GameFramework;
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class PlayerDataSerializationAdapter : PlayerDataSerializationAdapter<DefaultPlayerSerializableData, ConcretePlayerSerializableData>
    {
        public PlayerDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic) : base(dataType)
        {
        }

        protected override void FillFromSerializableDataForConcreteType(ConcretePlayerSerializableData data)
        {
            if (data == null)
            {
                Debug.LogWarning($"ConcretePlayerSerializableData is null. ");
                return;
            }

            var playerData = GameFrameworkSettings.PlayerDatabase.PlayerData;

            for (int i = 0; i < playerData.Length; i++)
            {
                switch(i)
                {
                    case 0: playerData[i].FillFromSerializableData(data.PlayTrialsData); break;
                    default:
                    break;
                }
            }
        }

        protected override ISerializableData GetSerializableDataForConcreteType(ConcretePlayerSerializableData data)
        {
            var playerData = GameFrameworkSettings.PlayerDatabase.PlayerData;

            for (int i = 0; i < playerData.Length; i++)
            {
                switch(i)
                {
                    case 0: data.PlayTrialsData = (playerData[i].GetSerializableData() as PlayTrialsSerializableData); break;
                    default:
                    break;
                }
            }

            return data;
        }
    }
}
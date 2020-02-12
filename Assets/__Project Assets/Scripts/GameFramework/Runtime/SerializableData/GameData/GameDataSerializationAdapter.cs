namespace GameFramework
{
  using d4160.Core;
  using d4160.GameFramework;
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class GameDataSerializationAdapter : GameDataSerializationAdapter<DefaultGameSerializableData, ConcreteGameSerializableData>
    {
        public GameDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic) : base(dataType)
        {
        }

        protected override ISerializableData GetSerializableDataForGenericType(DefaultGameSerializableData data)
        {
            var gameData = GameFrameworkSettings.GameDatabase.GameData;
            var serializableData = new BaseSerializableData[2];

            for (int i = 4; i < gameData.Length; i++)
            {
                serializableData[i - 4] = gameData[i].GetSerializableData() as BaseSerializableData;
            }

            data.SerializableData = serializableData;

            return data;
        }

        protected override void FillFromSerializableDataForGenericType(DefaultGameSerializableData data)
        {
            if (data == null || data.SerializableData == null)
            {
                Debug.LogWarning($"GameSerializableData is null. ");
                return;
            }

            var gameData = GameFrameworkSettings.GameDatabase.GameData;
            for (int i = 4; i < gameData.Length; i++)
            {
                if (!gameData.IsValidIndex(i)) break;

                gameData[i].FillFromSerializableData(data.SerializableData[i - 4]);
            }
        }

        protected override void FillFromSerializableDataForConcreteType(ConcreteGameSerializableData data)
        {
            if (data == null)
            {
                Debug.LogWarning($"ConcreteGameSerializableData is null. ");
                return;
            }

            var gameData = GameFrameworkSettings.GameDatabase.GameData;

            for (int i = 0; i < gameData.Length; i++)
            {
                switch(i)
                {
                    case 4: gameData[i].FillFromSerializableData(data.PlayersData); break;
                    case 5: gameData[i].FillFromSerializableData(data.LeaderboardsData); break;
                    default:
                    break;
                }
            }
        }

        protected override ISerializableData GetSerializableDataForConcreteType(ConcreteGameSerializableData data)
        {
            var gameData = GameFrameworkSettings.GameDatabase.GameData;

            for (int i = 0; i < gameData.Length; i++)
            {
                switch(i)
                {
                    case 4: data.PlayersData = (gameData[i].GetSerializableData() as PlayersSerializableData); break;
                    case 5: data.LeaderboardsData = (gameData[i].GetSerializableData() as LeaderboardsSerializableData); break;
                    default:
                    break;
                }
            }

            return data;
        }
    }
}
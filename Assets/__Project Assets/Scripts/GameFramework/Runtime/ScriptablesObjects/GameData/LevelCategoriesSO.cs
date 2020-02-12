namespace GameFramework
{
    using d4160.GameFramework;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New LevelCategories_SO.asset", menuName = "Game Framework/Game Data/LevelCategories")]
    public class LevelCategoriesSO : DefaultLevelCategoriesSO<DefaultLevelCategoriesReorderableArray, DefaultLevelCategory, DefaultLevelCategoriesSerializableData>
    {
        public override void Set(DefaultLevelCategoriesSerializableData data)
        {
        }

        public override DefaultLevelCategoriesSerializableData Get()
        {
            return null;
        }
    }
}
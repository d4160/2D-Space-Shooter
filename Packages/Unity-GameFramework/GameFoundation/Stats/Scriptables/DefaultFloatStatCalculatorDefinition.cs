using UnityEngine.GameFoundation;

namespace d4160.Systems.Flow
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game Framework/Game Data/Stat Calculators/DefaultFloatStat Definition")]
    public class DefaultFloatStatCalculatorDefinition : StatCalculatorDefinitionBase
    {
        public override float CalculateStat(int difficultyLevel = 1)
        {
            StatDefinition stat = Stat;
            InventoryItem item = Item;

            return item.GetStatFloat(stat.idHash);
        }
    }
}
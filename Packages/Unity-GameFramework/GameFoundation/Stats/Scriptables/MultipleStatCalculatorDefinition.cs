using UnityEngine.GameFoundation;

namespace d4160.GameFoundation
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game Framework/Game Data/Stat Calculators/MultipleStat Definition")]
    public class MultipleStatCalculatorDefinition : MultipleStatCalculatorDefinitionBase
    {
        public override float[] CalculateStats(int difficultyLevel = 1)
        {
            float[] stats = new float[_itemStatPairs.Length];
            
            if (_useFirstStatForAllCalculations)
            {
                StatDefinition stat = GetStat(0);
                InventoryItem item = GetItem(0);
                float value = default;

                if (StatManager.HasFloatValue(item, stat.idHash))
                {
                    value = item.GetStatFloat(stat.idHash);
                }
                else
                {
                    value = item.GetStatInt(stat.idHash);
                }
                
                for (var i = 0; i < _itemStatPairs.Length; i++)
                {
                    stats[i] = value;
                }
            }
            else
            {
                for (var i = 0; i < _itemStatPairs.Length; i++)
                {
                    StatDefinition stat = GetStat(i);
                    InventoryItem item = GetItem(i);

                    stats[i] = StatManager.HasFloatValue(item, stat.idHash) ? item.GetStatFloat(stat.idHash) : item.GetStatInt(stat.idHash); ;
                }
            }

            return stats;
        }

        public override float CalculateStat(int index, int difficultyLevel = 1)
        {
            StatDefinition stat = GetStat(index);
            InventoryItem item = GetItem(index);

            return StatManager.HasFloatValue(item, stat.idHash) ? item.GetStatFloat(stat.idHash) : item.GetStatInt(stat.idHash);
        }
    }
}
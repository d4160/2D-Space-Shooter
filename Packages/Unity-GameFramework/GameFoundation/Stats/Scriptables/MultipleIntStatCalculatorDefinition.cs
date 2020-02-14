﻿using UnityEngine.GameFoundation;

namespace d4160.Systems.Flow
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game Framework/Game Data/Stat Calculators/MultipleIntStat Definition")]
    public class MultipleIntStatCalculatorDefinition : MultipleStatCalculatorDefinitionBase
    {
        public override float[] CalculateStat(int difficultyLevel = 1)
        {
            float[] stats = new float[_itemStatPairs.Length];
            for (int i = 0; i < _itemStatPairs.Length; i++)
            {
                StatDefinition stat = GetStat(i);
                InventoryItem item = GetItem(i);

                stats[i] = item.GetStatInt(stat.idHash);
            }
            
            return stats;
        }

        public override float CalculateStat(int index, int difficultyLevel = 1)
        {
            StatDefinition stat = GetStat(index);
            InventoryItem item = GetItem(index);

            return item.GetStatInt(stat.idHash);
        }
    }
}
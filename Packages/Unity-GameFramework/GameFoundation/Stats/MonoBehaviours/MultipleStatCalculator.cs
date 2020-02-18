using System.Collections.Generic;
using System.Linq;
using d4160.Core;
using d4160.GameFramework;
using d4160.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.GameFoundation;
using UnityExtensions;

namespace d4160.GameFoundation
{
    public class MultipleStatCalculator : MonoBehaviour, IMultipleStatCalculator
    {
        [InspectInline(canEditRemoteTarget = true, canCreateSubasset = true)]
        [SerializeField] protected MultipleStatCalculatorDefinitionBase m_statCalculatorDefinition;
        [SerializeField] protected bool m_calculateAtStart;
        [SerializeField] protected int m_difficultyLevelToSetAtStart;
        [SerializeField] protected UltEvent m_onStatUpdated;

        protected float[] m_statValues;

        public UltEvent OnStatUpdated => m_onStatUpdated;

        public virtual float this[int index]
        {
            get => m_statValues.IsValidIndex(index) ? m_statValues[index] : 0f;
            set
            {
                if (m_statValues.IsValidIndex(index))
                {
                    m_statValues[index] = value;
                    m_onStatUpdated?.Invoke(index, value);
                }
            }
        }

        public virtual float[] FloatStats
        {
            get => m_statValues;
            set
            {
                m_statValues = value;
                for (int i = 0; i < m_statValues.Length; i++)
                {
                    m_onStatUpdated?.Invoke(i, m_statValues[i]);
                }
            }
        }
        
        public virtual int GetIntStat(int index) => (int)this[index];
        public virtual void SetIntStat(int index, int value) => this[index] = value;

        protected virtual void Start()
        {
            if (!m_calculateAtStart) return;

            if (!InventoryManager.IsInitialized)
            {
                DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls += CalculateStatsAtStart;
            }
            else
            {
                CalculateStatsAtStart();
            }
        }

        protected virtual void OnDestroy()
        {
            if (InventoryManager.IsInitialized)
                DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls -= CalculateStatsAtStart;
        }

        protected void CalculateStatsAtStart()
        {
            CalculateStats(m_difficultyLevelToSetAtStart);
        }

        public virtual float[] CalculateStats(int difficultyLevel = 1)
        {
            if (m_statCalculatorDefinition)
            {
                FloatStats = m_statCalculatorDefinition.CalculateStats(difficultyLevel);
            }

            return FloatStats;
        }

        public virtual float CalculateStat(int index, int difficultyLevel = 1)
        {
            if (m_statCalculatorDefinition)
            {
                this[index] = m_statCalculatorDefinition.CalculateStat(index, difficultyLevel);
            }

            return this[index];
        }

        public virtual void UpdateStat(float deltaTime)
        {
            float diff = deltaTime;
            for (int i = 0; i < FloatStats.Length; i++)
            {
                FloatStats[i] += diff;
            }
        }

        public virtual int AddStat(int statIndex = 0, int difficultyLevel = 1)
        {
            List<float> list = null;
            list = m_statValues != null ? new List<float>(m_statValues) {default} : new List<float>(1){ default };

            m_statValues = list.ToArray();

            this[m_statValues.LastIndex()] = m_statCalculatorDefinition.CalculateStat(statIndex, difficultyLevel);

            return m_statValues.LastIndex();
        }

        [System.Serializable]
        public class UltEvent : UltEvents.UltEvent<int, float>
        {
        }
    }
}

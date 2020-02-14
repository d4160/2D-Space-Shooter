using d4160.Core;
using d4160.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityExtensions;

namespace d4160.Systems.Flow
{
    public class MultipleStatCalculatorBase : MonoBehaviour, IMultipleStatCalculator
    {
        [InspectInline(canEditRemoteTarget = true, canCreateSubasset = true)]
        [SerializeField] protected MultipleStatCalculatorDefinitionBase m_statCalculatorDefinition;
        [SerializeField] protected bool m_calculateAtStart;
        [SerializeField] protected int m_difficultyLevelToSetAtStart;
        [SerializeField] protected bool m_oneStatDefinitionForAll;
        [SerializeField] protected UnityEvent m_onStatUpdated;

        protected float[] m_statValues;

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
            if (m_calculateAtStart)
                CalculateStat(m_difficultyLevelToSetAtStart);
        }

        public virtual float[] CalculateStat(int difficultyLevel = 1)
        {
            if (m_statCalculatorDefinition)
                FloatStats = m_statCalculatorDefinition.CalculateStat(difficultyLevel);

            return FloatStats;
        }

        public virtual float CalculateStat(int index, int difficultyLevel = 1)
        {
            if (m_statCalculatorDefinition)
                this[index] = m_statCalculatorDefinition.CalculateStat(index, difficultyLevel);

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

        [System.Serializable]
        public class UnityEvent : UnityEvent<int, float>
        {
        }
    }
}

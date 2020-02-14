using d4160.Core;
using d4160.Loops;
using d4160.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace d4160.Systems.Flow
{
    public class MultipleTimerCalculator : MultipleStatCalculatorBase
    {
        [SerializeField] protected bool m_activeWhenCalculated;
        [SerializeField] protected UnityEvent m_onTimerCalculated;
        [SerializeField] protected UnityUtilities.IntEvent m_onTimerOver;

        protected bool[] _activeStates;

        private void OnEnable()
        {
            UpdateLoop.OnUpdate += UpdateStat;
        }

        private void OnDisable()
        {
            UpdateLoop.OnUpdate -= UpdateStat;
        }

        public override void UpdateStat(float deltaTime)
        {
            for (int i = 0; i < FloatStats.Length; i++)
            {
                if (!_activeStates[i]) continue;

                if (FloatStats[i] > 0)
                {
                    FloatStats[i] -= deltaTime;

                    if (FloatStats[i] <= 0)
                    {
                        FloatStats[i] = 0;

                        m_onTimerOver?.Invoke(i);
                    }
                }
            }
        }

        public override float[] CalculateStat(int difficultyLevel = 1)
        {
            var stats = base.CalculateStat(difficultyLevel);
            _activeStates = new bool[stats.Length];

            for (int i = 0; i < stats.Length; i++)
            {
                _activeStates[i] = m_activeWhenCalculated;
                m_onTimerCalculated?.Invoke(i, stats[i]);
            }

            return stats;
        }

        public override float CalculateStat(int index, int difficultyLevel = 1)
        {
            var stat = base.CalculateStat(index, difficultyLevel);

            _activeStates[index] = m_activeWhenCalculated;

            m_onTimerCalculated?.Invoke(index, stat);

            return stat;
        }

        public virtual void SetAllTimersActive(bool active)
        {
            for (var i = 0; i < _activeStates.Length; i++)
            {
                _activeStates[i] = active;
            }
        }

        public virtual void SetTimerActive(int index, bool active)
        {
            if (_activeStates.IsValidIndex(index))
            {
                _activeStates[index] = active;
            }
        }
    }
}
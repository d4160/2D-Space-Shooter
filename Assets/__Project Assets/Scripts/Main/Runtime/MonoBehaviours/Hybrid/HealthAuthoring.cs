using d4160.GameFoundation;
using d4160.GameFramework;

public class HealthAuthoring : AuthoringBehaviourBase<Health>, IMultipleStatUpgradeable
{
    public void Damage()
    {
        _data.lives--;

        if (_data.lives < 1)
        {
            EnemySpawner.Instance.StopSpawnRoutine();

            Destroy(gameObject);
        }
    }

    public void UpdateStat(int index, float value)
    {
        if (index != 0) return;

        _data.lives = (int)value;
    }
}

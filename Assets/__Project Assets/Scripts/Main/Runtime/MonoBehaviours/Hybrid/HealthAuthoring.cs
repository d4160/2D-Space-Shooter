using d4160.GameFoundation;
using d4160.GameFramework;
using UltEvents;
using UnityEngine;
using UnityEngine.GameFoundation;

[RequireComponent(typeof(DefaultDestroyable))]
public class HealthAuthoring : AuthoringBehaviourBase<Health>, IMultipleStatUpgradeable
{
    [SerializeField] protected IntFloatUltEvent _onHealthUpdated;

    protected int _invulnerableItem;
    protected DefaultDestroyable _destroyable;

    public IntFloatUltEvent OnHealthUpdated => _onHealthUpdated;

    protected virtual void Awake()
    {
        _destroyable = GetComponent<DefaultDestroyable>();
    }

    public virtual void SetInvulnerable(bool value, int itemHash = 0)
    {
        _data.isInvulnerable = value;

        if (value)
            _invulnerableItem = itemHash;
    }

    public virtual void Damage(int damage = 1)
    {
        DamageInternal(damage);

        CheckForDestroy();
    }

    protected virtual void DamageInternal(int damage)
    {
        if (_data.isInvulnerable)
        {
            Inventory.main.RemoveItem(_invulnerableItem);
            return;
        }

        _data.lives -= damage;

        if (_data.lives < 0) return;

        _onHealthUpdated?.Invoke(0, _data.lives);
    }

    protected virtual void CheckForDestroy()
    {
        if (_data.lives < 1)
        {
            _destroyable.Destroy();
        }
    }

    public virtual void UpdateStat(int index, float value)
    {
        if (index != 0) return;

        _data.lives = (int)value;
    }
}

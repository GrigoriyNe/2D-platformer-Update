using UnityEngine;

public class HealthKit : CllectableItem
{
    [SerializeField] private Health _health;

    public float RecoverHeal { get; private set; } = 20;

    private void OnEnable()
    {
        this.Changed += Healing;
    }

    private void OnDisable()
    {
        this.Changed -= Healing;
    }

    private void Healing(CllectableItem cllectable)
    {
        _health.RestoreHeal(RecoverHeal);
    }
}
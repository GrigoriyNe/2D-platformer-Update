using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _min = 0;

    public event UnityAction<float> Changed;
    public event UnityAction<float> ChangedRestore;

    public float Max { get; private set; } = 100;
    public float Value { get; private set; }

    private void Awake()
    {
        Value = Max;
    }

    public void TakeDamage(float damage)
    {
        Value = Mathf.Clamp(Value - damage, _min, Max);
        Changed?.Invoke(Value);
    }

    public void RestoreHeal(float heal)
    {
        Value = Mathf.Clamp(Value + heal, _min, Max);
        ChangedRestore?.Invoke(Value);
    }
}

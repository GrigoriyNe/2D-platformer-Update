using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _max = 100;
    private float _min = 0;

    public float Value { get; private set; }

    public event UnityAction<float> Changed;

    private void Awake()
    {
        Value = _max;
    }

    public void TakeDamage(float damage)
    {
        Value -= Mathf.Clamp(damage, _min, _max);
        Changed?.Invoke(Value);
    }

    public void RestoreHeal(float heal)
    {
        if (Value + heal > _max)
        {
            Value = _max;
            Debug.Log("Текущее здоровье максимально: " + Value);
        }
        else
        {
            Value += Mathf.Clamp(heal, _min, _max);
            Debug.Log("Полненно здоровье. Текущее: " + Value);
        }
    }
}

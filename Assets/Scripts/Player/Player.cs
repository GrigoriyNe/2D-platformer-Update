using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    private Health _health;
    private bool _isAtacked;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Changed += TakeDamage;
    }

    private void OnDisable()
    {
        _health.Changed -= TakeDamage;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Получен урон. Здоровья: " + _health.Value);

        if (_health.Value <= 0)
        {
            gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }
}
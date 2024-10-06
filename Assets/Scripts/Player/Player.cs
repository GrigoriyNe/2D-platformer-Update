using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    private bool _isAtacked;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        Health.Changed += TakeDamage;
    }

    private void OnDisable()
    {
        Health.Changed -= TakeDamage;
    }

    public void TakeDamage(float damage)
    {
        if (Health.Value <= 0)
        {
            gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }
}
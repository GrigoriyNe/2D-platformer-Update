using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private Health _health;
    private int _delayDeath = 2;

    public bool IsAlive => _health.Value > 0;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_health.Value <= 0)
        {
            StartCoroutine(WaitDeath());
        }
    }

    private IEnumerator WaitDeath()
    {
        var wait = new WaitForSeconds(_delayDeath);
        yield return wait;

        gameObject.SetActive(false);
    }
}

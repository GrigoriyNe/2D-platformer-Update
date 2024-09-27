using System.Collections;
using UnityEngine;

public class EnemyAtacker : MonoBehaviour
{
    [SerializeField] private EnemyAnimationDirect _animationDirect;
    [SerializeField] private Health _health;

    private float _damage = 5;
    private bool _isPunching = false;

    public void Update()
    {
        _isPunching = true;
    }

    private void OnTriggerStay2D(Collider2D collision2D)
    {
        if (_isPunching && _health.Value > 0)
        {
            Player enemy = collision2D.gameObject.GetComponent<Player>();
            Health healthEnemy = enemy.GetComponent<Health>();

            if (enemy != null)
            {
                _animationDirect.SetAnimationAtack(_isPunching);
                healthEnemy.TakeDamage(_damage * Time.deltaTime);
            }

            StartCoroutine(WaitAtackAnimation());
        }
    }

    private IEnumerator WaitAtackAnimation()
    {

        var wait = new WaitForSeconds(1);
        yield return wait;

        _isPunching = false;
        _animationDirect.SetAnimationAtack(_isPunching);
    }
}


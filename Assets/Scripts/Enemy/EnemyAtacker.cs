using System.Collections;
using UnityEngine;

public class EnemyAtacker : MonoBehaviour
{
    [SerializeField] private EnemyAnimationDirect _animationDirect;
    [SerializeField] private Health _health;

    private float _damage = 5f;
    private float _delayAnimation = 1f;
    private float _delayAtack = 0.1f;
    private bool _isPunching = false;

    public void Update()
    {
        _isPunching = true;
    }

    private void OnTriggerStay2D(Collider2D collision2D)
    {
        if (_isPunching && _health.Value > 0)
        {
            Player player = collision2D.gameObject.GetComponent<Player>();

            if (player != null)
            {
                StartCoroutine(AtackWhithDelay(player));
            }
        }
    }

    private IEnumerator AtackWhithDelay(Player player)
    {
        var waitAtack = new WaitForSeconds(_delayAtack);
        var wait = new WaitForSeconds(_delayAnimation);

        yield return waitAtack;

        _animationDirect.SetAnimationAtack(_isPunching);
        player.Health.TakeDamage(_damage * Time.deltaTime);

        yield return wait;

        _isPunching = false;
        _animationDirect.SetAnimationAtack(_isPunching);
    }
}


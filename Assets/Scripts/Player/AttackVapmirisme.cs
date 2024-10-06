using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackVapmirisme : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Player _player;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private LayerMask maskEnemy;
    [SerializeField] private float _damage;
    [SerializeField] private float _smoothValue;

    private int _cooldown = 3;
    private float _radius = 2;
    public int TimeAtack { get; private set; } = 6;
    public Coroutine Coroutine { get; private set; } = null;

    public event UnityAction<bool> IsSpesialAtackIsRuning;

    private void OnEnable()
    {
        _sprite.gameObject.SetActive(false);
        _inputReader.IsSpecialAtack += Atack;
    }

    private void OnDisable()
    {
        _inputReader.IsSpecialAtack -= Atack;
    }

    private void Atack(bool isAtack)
    {
        if (isAtack && Coroutine == null)
        {
            Coroutine = StartCoroutine(SpesialAtackWithDelay());
            IsSpesialAtackIsRuning?.Invoke(true);
        }
    }

    private IEnumerator SpesialAtackWithDelay()
    {
        float timer = 0;
        var waitCooldown = new WaitForSecondsRealtime(_cooldown);

        _sprite.gameObject.SetActive(true);

        while (TimeAtack >= timer)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, maskEnemy);

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out Enemy enemy))
                {
                    enemy.Health.TakeDamage(Mathf.MoveTowards(0f, _damage, _smoothValue * Time.deltaTime));
                    _player.Health.RestoreHeal(Mathf.MoveTowards(0f, _damage, _smoothValue * Time.deltaTime));
                }
            }
            yield return null;

            timer += Time.deltaTime;
        }

        _sprite.gameObject.SetActive(false);

        yield return waitCooldown;

        Coroutine = null;
    }
}


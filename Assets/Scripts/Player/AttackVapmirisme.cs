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
    private Enemy _target;
    private WaitForSecondsRealtime _waitCooldown;

    public Coroutine Coroutine { get; private set; } = null;
    public int TimeAtack { get; private set; } = 6;

    public event UnityAction<bool> IsSpesialAtackIsRuning;

    private void Awake()
    {
        _sprite.gameObject.SetActive(false);
        _waitCooldown = new WaitForSecondsRealtime(_cooldown);
        _target = null;
    }

    private void OnEnable()
    {
        _inputReader.IsSpecialAtack += Atack;
    }

    private void OnDisable()
    {
        _inputReader.IsSpecialAtack -= Atack;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (_target == null)
            {
                _target = enemy;
            }
            else
            {
                if (Vector2.Distance(_player.transform.position, enemy.transform.position)
                    < Vector2.Distance(_player.transform.position, _target.transform.position))
                {
                    _target = enemy;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _target = null;
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

        _sprite.gameObject.SetActive(true);

        while (TimeAtack >= timer)
        {
            if (_target != null)
            {
                if (_target.Health.Value < _damage)
                {
                    _player.Health.RestoreHeal(_target.Health.Value);
                }
                else
                {
                    _player.Health.RestoreHeal(_damage);
                }

                _target.Health.TakeDamage(_damage);
            }

            yield return timer += Time.deltaTime;
        }

        _sprite.gameObject.SetActive(false);

        yield return _waitCooldown;

        Coroutine = null;
    }
}

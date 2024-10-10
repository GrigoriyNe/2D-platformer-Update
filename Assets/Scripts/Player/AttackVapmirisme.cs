using System;
using System.Collections;
using UnityEngine;

public class AttackVapmirisme : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _maskEnemy;
    [SerializeField] private float _damage;

    private WaitForSecondsRealtime _waitCooldown;
    private WaitForSecondsRealtime _delayOverlap;
    private Coroutine _coroutineOverlapDelay;
    private Coroutine _coroutineWaitCooldown;
    private Enemy _target;
    private Coroutine _coroutineAttack  = null;

    private int _quantityTargetOfAttack = 1;
    private int _radius = 2;
    private int _cooldown = 3;
    private float _timer = 0;
    private float _maxDistanse = 3f;
    private float _delayOverlapValue = 0.5f;

    public event Action<float> IsSpesialAttackIsRuning;
    public event Action<float> IsCooldownChange;

    public int TimeAttack { get; private set; } = 6;

    private void Awake()
    {
        _delayOverlap = new WaitForSecondsRealtime(_delayOverlapValue);
        _waitCooldown = new WaitForSecondsRealtime(_cooldown);
        _target = null;
    }

    private void OnEnable()
    {
        _input.IsSpecialAttack += Attack;
    }

    private void OnDisable()
    {
        _input.IsSpecialAttack -= Attack;
    }

    private void Attack()
    {
        if (_coroutineAttack == null)
            _coroutineAttack = StartCoroutine(SpesialAttackWithDelay());
    }

    private float SqrDistance(Vector3 start, Vector3 end)
    {
        return (end - start).sqrMagnitude;
    }

    private IEnumerator TryFindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _maskEnemy);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                if (_target == null)
                {
                    _target = enemy;
                }
                else
                {
                    if (SqrDistance(_player.transform.position, _target.transform.position) > _maxDistanse)
                    {
                        _target = null;
                    }

                    if (hits.Length > _quantityTargetOfAttack)
                    {
                        if (SqrDistance(_player.transform.position, enemy.transform.position)
                        < SqrDistance(_player.transform.position, _target.transform.position))
                        {
                            _target = enemy;
                        }
                    }
                }
            }
        }

        yield return _delayOverlap;
    }

    private IEnumerator SpesialAttackWithDelay()
    {
        while (TimeAttack > _timer)
        {
            _coroutineOverlapDelay = StartCoroutine(TryFindTarget());
            IsSpesialAttackIsRuning?.Invoke(_timer);

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

            yield return null;

            _timer += Time.deltaTime;
        }

        _coroutineOverlapDelay = null;
        _target = null;
        _coroutineWaitCooldown = StartCoroutine(WaitCooldown());
    }

    private IEnumerator WaitCooldown()
    {
        while (_timer > 0)
        {
            _timer -= (TimeAttack / _cooldown) * Time.deltaTime;
            IsCooldownChange?.Invoke(_timer);
            yield return null;
        }

        _coroutineAttack = null;
        _waitCooldown = null;
    }
}

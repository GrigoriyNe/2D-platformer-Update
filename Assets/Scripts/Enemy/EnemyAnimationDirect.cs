using System.Collections;
using UnityEngine;

public class EnemyAnimationDirect : MonoBehaviour
{
    private const string States = nameof(States);
    private const string IsAtacked = nameof(IsAtacked);
    private const string IsDead = nameof(IsDead);
    private const int StateOfWalk = 1;
    private const int StateOfAttack = 3;

    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;

    private bool _isAtacked = false;
    private bool _isAtack = false;
    private bool _isWalk = false;
    private bool _isDead = false;
    private int _dealayTakeDamageAnimation = 1;

    private void OnEnable()
    {
        _health.Changed += SetAnimationAtacked;
    }

    private void OnDisable()
    {
        _health.Changed -= SetAnimationAtacked;
    }

    private void Update()
    {
        if (_isDead == false)
        {
            if (_isWalk)
                _animator.SetInteger(States, StateOfWalk);

            if (_isAtack)
                _animator.SetInteger(States, StateOfAttack);
        }
        else
        {
            _animator.SetBool(IsDead, _isDead);
        }
    }

    public void SetAnimationAtacked(float valueHealth)
    {
        _isAtacked = true;
        _animator.SetBool(IsAtacked, _isAtacked);
        StartCoroutine(WaitTakeDamageAnimation());

        if (valueHealth <= 0)
            _isDead = true;
    }

    public void SetAnimationWalk()
    {
        _isWalk = true; 
    }

    public void SetAnimationAtack(bool isAtack)
    {
        if (isAtack && _isAtacked == false)
            _isAtack = true;
        else
            _isAtack = false;
    }

    public IEnumerator WaitTakeDamageAnimation()
    {
        var wait = new WaitForSecondsRealtime(_dealayTakeDamageAnimation);
        yield return wait;

        _isAtacked = false;
        _animator.SetBool(IsAtacked, _isAtacked);
    }
}

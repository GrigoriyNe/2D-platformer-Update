using System.Collections;
using UnityEngine;

public class AnimationDirect : MonoBehaviour
{
    private const string States = nameof(States);
    private const string IsAtacked = nameof(IsAtacked);
    private const string IsHeal = nameof(IsHeal);
    private const int StateOfIdle = 0;
    private const int StateOfWalk = 1;
    private const int StateOfRun = 2;
    private const int StateOfAttack = 3;

    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;
    [SerializeField] private InputReader _inputReader;

    private bool _isAtacked;
    private bool _isHeal;
    private bool _isRunning = false;
    private int _delayTakeDamageAnimation = 1;

    private void OnEnable()
    {
        _health.Changed += GetTakeDamageAnimation;
        _health.ChangedRestore += GetHealAnimation;
        _inputReader.IsAtack += GetAtackAnimation;
        _inputReader.IsRunning += GetRunAnimation;
        _inputReader.Direction += GetWalkAnimation;
    }

    private void OnDisable()
    {
        _health.Changed -= GetTakeDamageAnimation;
        _health.ChangedRestore -= GetHealAnimation;
        _inputReader.IsAtack -= GetAtackAnimation;
        _inputReader.IsRunning -= GetRunAnimation;
        _inputReader.Direction -= GetWalkAnimation;
    }

    private void GetAtackAnimation(bool isAtack)
    {
        if (isAtack)
            _animator.SetInteger(States, StateOfAttack);
    }

    private void GetWalkAnimation(Vector2 direction)
    {
        _animator.SetInteger(States, StateOfIdle);

        if (direction.x != 0)
        {
            if (_isRunning)
            {
                _animator.SetInteger(States, StateOfRun);
            }
            else
            {
                _animator.SetInteger(States, StateOfWalk);
            }
        }
    }

    private void GetRunAnimation(bool isRunning)
    {
        if (isRunning)
            _isRunning = isRunning;
    }

    private void GetTakeDamageAnimation(float health)
    {
        _isAtacked = health > 0;
        _animator.SetBool(PlayerAnimatorData.Params.IsAtacked, _isAtacked);
        StartCoroutine(WaitDamageAnimation());
    }

    private void GetHealAnimation(float health)
    {
        _isHeal = true;
        _animator.SetBool(PlayerAnimatorData.Params.IsHeal, _isHeal);
        StartCoroutine(WaitHealAnimation());
    }

    private IEnumerator WaitDamageAnimation()
    {
        var wait = new WaitForSecondsRealtime(_delayTakeDamageAnimation);

        yield return wait;

        _isAtacked = false;
        _animator.SetBool(PlayerAnimatorData.Params.IsAtacked, _isAtacked);
    }

    private IEnumerator WaitHealAnimation()
    {
        var wait = new WaitForSecondsRealtime(_delayTakeDamageAnimation);

        yield return wait;

        _isHeal = false;
        _animator.SetBool(PlayerAnimatorData.Params.IsHeal, _isHeal);
    }
}

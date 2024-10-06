using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode Run = KeyCode.LeftShift;
    private const KeyCode Jump = KeyCode.Space;
    private const int ValueOfLeftClickMouse = 0;
    private const int ValueOfRigthClickMouse = 1;
    
    public event UnityAction<Vector2> Direction;
    public event UnityAction<bool> IsRunning;
    public event UnityAction<bool> IsJumpPressed;
    public event UnityAction<bool> IsAtack;
    public event UnityAction<bool> IsSpecialAtack;

    private Vector2 _startVector = Vector2.zero;
    private float _delayAttack = 0.1f;

    private void Update()
    {
        TryMove();
        TryRun();
        TryJump();
        TryAtack();
        TrySpecialAtack();
    }

    private void TryMove()
    {
        Direction?.Invoke(_startVector);

        if (Input.GetAxis(Horizontal) != 0)
            Direction?.Invoke(new Vector2(Input.GetAxis(Horizontal), 0));
    }

    private void TryRun()
    {
        IsRunning?.Invoke(false);

        if (Input.GetKey(Run))
            IsRunning?.Invoke(true);
    }

    private void TryJump()
    {
        IsJumpPressed?.Invoke(false);

        if (Input.GetKey(Jump))
            IsJumpPressed?.Invoke(true);
    }

    private void TryAtack()
    {
        IsAtack?.Invoke(false);

        if (Input.GetMouseButton(ValueOfLeftClickMouse))
            StartCoroutine(AttackWihtDelay());
    }

    private void TrySpecialAtack()
    {
        IsSpecialAtack?.Invoke(false);

        if (Input.GetMouseButton(ValueOfRigthClickMouse))
            IsSpecialAtack?.Invoke(true);
    }

    private IEnumerator AttackWihtDelay()
    {
        var wait = new WaitForSecondsRealtime(_delayAttack);

        yield return wait;
        IsAtack?.Invoke(true);
    }
}

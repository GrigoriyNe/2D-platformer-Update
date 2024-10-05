using UnityEngine;

[RequireComponent(
    typeof(Rigidbody2D),
    typeof(InputReader))]
public class PlayerMover : MonoBehaviour
{
    private readonly Vector2 Force = new Vector2(0, 50);

    private Rigidbody2D _rigidbody;
    private InputReader _inputReader;

    private int _degreeTurn = 180;
    private int _speed;
    private int _deafultSpeed = 2;
    private bool _isGround = false;
    private bool _isJumpPressed = false;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();

        _inputReader.Direction += Move;
        _inputReader.IsRunning += Run;
        _inputReader.IsJumpPressed += TryJump;
    }

    private void OnDisable()
    {
        _inputReader.Direction -= Move;
        _inputReader.IsRunning -= Run;
        _inputReader.IsJumpPressed -= TryJump;
    }

    private void FixedUpdate()
    {
        TryGround();

        if (_isGround && _isJumpPressed)
            _rigidbody.AddForce(Force);
    }

    private void Move(Vector2 directionInput)
    {
        Vector2 direction = directionInput;
        Vector2 startRotate = transform.eulerAngles;
        Vector2 rotate = transform.eulerAngles;
        startRotate.y = 0;
        rotate.y = _degreeTurn;

        if (direction.x != 0)
        {
            transform.rotation = (Quaternion.Euler(startRotate));
            transform.Translate(_speed * Time.deltaTime * direction);

            if (direction.x <= 0)
            {
                int factorNegative = -2;
                transform.rotation = (Quaternion.Euler(rotate));
                transform.Translate(_speed * Time.deltaTime * direction / factorNegative);
            }
        }
    }

    private void Run(bool IsRunningPress)
    {
        bool isRunning = IsRunningPress;

        if (isRunning && _isGround)
        {
            int _runSpeed = 2;
            _speed = _deafultSpeed * _runSpeed;
        }
        else
        {
            _speed = _deafultSpeed;
        }
    }

    private void TryGround()
    {
        int rayDistance = 1;
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
            _isGround = true;
        else
            _isGround = false;
    }

    private void TryJump(bool jumpPress)
    {
        _isJumpPressed = jumpPress;
    }
}

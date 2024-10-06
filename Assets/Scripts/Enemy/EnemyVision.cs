using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private Vector2 _target;
    private bool _isPlayerSee = false;

    public Vector2 Target => _target;
    public bool IsPlayerSee => _isPlayerSee;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            _isPlayerSee = true;
            _target = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerSee = false;
    }
}

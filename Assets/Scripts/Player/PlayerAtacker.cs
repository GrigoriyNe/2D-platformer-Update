using UnityEngine;

public class PlayerAtacker : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask maskEnemy;

    private float _damage = 50;

    private void OnEnable()
    {
        _inputReader.IsAtack += Attack;
    }

    private void OnDisable()
    {
        _inputReader.IsAtack -= Attack;
    }

    private void Attack(bool isAttack)
    {
        if (isAttack)
        {
            float radius = 1;
            Collider2D[] collisions2D = Physics2D.OverlapCircleAll(transform.position, radius, maskEnemy);

            foreach (Collider2D collider in collisions2D)
            {
                collider.gameObject.TryGetComponent(out Enemy enemy);

                if (enemy != null)
                {
                    enemy.Health.TakeDamage(_damage * Time.deltaTime);
                }
            }
        }
    }
}
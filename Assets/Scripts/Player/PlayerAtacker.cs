using UnityEngine;

public class PlayerAtacker : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

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
            Collider2D[] collisions2D = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D collider in collisions2D)
            {
                Enemy enemy = collider.gameObject.GetComponent<Enemy>();

                if (enemy != null)
                {
                    Health healthEnemy = enemy.GetComponent<Health>();
                    healthEnemy.TakeDamage(_damage * Time.deltaTime);
                }
            }
        }
    }
}
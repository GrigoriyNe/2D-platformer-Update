using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private HealthKit _healthKit;
    [SerializeField] private Coin _coin;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out CllectableItem item))
        {
            if (item.GetType() == typeof(HealthKit))
            {
                _health.RestoreHeal(_healthKit.RecoverHeal);
            }
            else if (item.GetType() == typeof(Coin))
            {
                _wallet.PutMoney(_coin.Coust);
            }

            item.Take();
        }
    }
}

using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.TryGetComponent(out HealthKit healthKit))
        {
            healthKit.Take();
        }
        if (other.gameObject.TryGetComponent(out Coin coin))
        {
            coin.Take();
        }
    }
}

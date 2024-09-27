using UnityEngine;
using UnityEngine.Events;

public class CllectableItem : MonoBehaviour
{
    public event UnityAction<CllectableItem> Changed;

    public void Init(Vector2 start)
    {
        transform.position = start;
        transform.rotation = Quaternion.identity;
    }

    public void Take()
    {
        Changed?.Invoke(this);
    }
}

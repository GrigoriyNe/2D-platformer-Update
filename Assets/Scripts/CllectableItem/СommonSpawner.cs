using UnityEngine;
using UnityEngine.Pool;

public class ÑommonSpawner : MonoBehaviour
{
    [SerializeField] private CllectableItem _object;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _poolMaxSize = 5;
    [SerializeField] private int _poolCapacity = 3;

    private ObjectPool<CllectableItem> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<CllectableItem>(
            createFunc: () => Instantiate(_object),
            actionOnGet: (obj) => OnGet(obj),
            actionOnRelease: (obj) => OnRealise(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

        while (_pool.CountActive < _poolMaxSize)
        {
            _pool.Get();
        }
    }

    private void OnGet(CllectableItem obj)
    {
        obj.gameObject.SetActive(true);
        obj.Init(GetRandomStartPosition());
        obj.Changed += ChangePool;
    }

    private void OnRealise(CllectableItem obj)
    {
        obj.gameObject.SetActive(false);
        obj.Changed -= ChangePool;
        _audioSource.Play();
    }

    private void ChangePool(CllectableItem obj)
    {
        _pool.Release(obj);
        _pool.Get();
    }

    private Vector3 GetRandomStartPosition()
    {
        int minRandomValueX = -8;
        int maxRandomValueX = 8;
        int minRandomValueY = -4;
        int maxRandomValueY = 4;
        int valueRandomX = Random.Range(minRandomValueX, maxRandomValueX + 1);
        int valueRandomY = Random.Range(minRandomValueY, maxRandomValueY + 1);

        return new Vector3(
            transform.position.x + valueRandomX,
            transform.position.y + valueRandomY,
            transform.position.z);
    }
}
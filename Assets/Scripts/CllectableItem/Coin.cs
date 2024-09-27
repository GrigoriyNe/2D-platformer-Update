using UnityEngine;

public class Coin : CllectableItem
{
    [SerializeField] private Wallet _wallet;

    public int Coust { get; private set; } = 1;

    private void OnEnable()
    {
        this.Changed += Loot;
    }

    private void OnDisable()
    {
        this.Changed -= Loot;
    }

    private void Loot(CllectableItem cllectable)
    {
        _wallet.PutMoney(Coust);
    }
}
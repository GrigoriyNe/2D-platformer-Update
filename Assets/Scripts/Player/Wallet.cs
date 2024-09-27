using UnityEngine;

public class Wallet : MonoBehaviour
{
    private float _allMoney;

    public void PutMoney(float profit)
    {
        _allMoney += profit;
        Debug.Log("Монета в кармане. Всего: " + _allMoney);
    }
}

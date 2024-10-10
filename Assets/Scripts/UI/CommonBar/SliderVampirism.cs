using UnityEngine;
using UnityEngine.UI;

public class SliderVampirism : MonoBehaviour
{
    [SerializeField] private AttackVapmirisme _vapmirisme;

    private Slider _slider;

    private void OnEnable()
    {
        _vapmirisme.IsSpesialAttackIsRuning += ValueChanged;
        _vapmirisme.IsCooldownChange += ValueChanged;
    }

    private void OnDisable()
    {
        _vapmirisme.IsSpesialAttackIsRuning -= ValueChanged;
        _vapmirisme.IsCooldownChange -= ValueChanged;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void ValueChanged(float timer)
    {
        _slider.value = timer / _vapmirisme.TimeAttack;
    }
}

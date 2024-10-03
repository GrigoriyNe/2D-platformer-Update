using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBarAbstract : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _text;
    public Slider Slider { get; private set; }

    private void Awake()
    {
        Slider = GetComponent<Slider>();
        Slider.maxValue = _health.Max;
        Slider.value = Slider.maxValue;
    }

    private void OnEnable()
    {
        _health.Changed += ChandgedHealthValue;
        _health.ChangedRestore += ChandgedHealthValue;
        _text.text = _health.Max.ToString();
    }

    private void OnDisable()
    {
        _health.Changed -= ChandgedHealthValue;
        _health.ChangedRestore -= ChandgedHealthValue;
    }
    public abstract void ChangeSlider(float healthValue);

    private void ChandgedHealthValue(float healthValue)
    {
        int healthForView = Convert.ToInt32(_health.Value);
        _text.text = (healthForView.ToString());
        ChangeSlider(healthValue);
    }
}

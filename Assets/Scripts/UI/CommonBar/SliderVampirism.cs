using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderVampirism : MonoBehaviour
{
    [SerializeField] private AttackVapmirisme _vapmirisme;

    private Slider _slider;
    private float _smoothValue = 2.6f;

    private void OnEnable()
    {
        _vapmirisme.IsSpesialAtackIsRuning += ValueChanged;
    }
    private void OnDisable()
    {
        _vapmirisme.IsSpesialAtackIsRuning -= ValueChanged;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _vapmirisme.TimeAtack;
        _slider.value = _slider.maxValue;
    }

    private void ValueChanged(bool isRuning)
    {
        StartCoroutine(ChangeSliderSmooth(isRuning));
    }

    private IEnumerator ChangeSliderSmooth(bool isRuning)
    {
        if (isRuning)
        {
            _slider.value = _slider.maxValue;

            while (_slider.value != 0)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, 0, Time.deltaTime);

                yield return null;
            }

            _slider.value = 0;

            while (_slider.value != _vapmirisme.TimeAtack)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, _vapmirisme.TimeAtack, _smoothValue * Time.deltaTime);

                yield return null;
            }
        }
    }
}

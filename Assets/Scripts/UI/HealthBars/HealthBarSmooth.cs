using System.Collections;
using UnityEngine;

public class HealthBarSmooth : HealthBarAbstract
{
    [SerializeField, Min(10f)] private float _smoothValue = 50f;

    private Coroutine _coroutine;

    public override void ChangeSlider(float healthValue)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (healthValue == 0)
            gameObject.SetActive(false);

        if (healthValue > 0)
            _coroutine = StartCoroutine(ChangeSliderSmooth(healthValue));
    }

    private IEnumerator ChangeSliderSmooth(float healthValue)
    {
        while (Slider.value != healthValue)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, healthValue, _smoothValue * Time.deltaTime);

            yield return null;
        }
    }
}

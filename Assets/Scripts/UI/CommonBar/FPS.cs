using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FPS : MonoBehaviour
{
    private TMP_Text _fpsText;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    private float _delay = 0.2f;

    private void Start()
    {
        _wait = new WaitForSeconds(_delay);
        _fpsText = GetComponent<TMP_Text>();
        _coroutine = StartCoroutine(FramesPerSecond());
    }

    private IEnumerator FramesPerSecond()
    {
        while (enabled)
        {
            int fps = (int)(1f / Time.deltaTime);
            DisplayFPS(fps);


            yield return _wait;
        }
    }

    private void DisplayFPS(float fps)
    {
        _fpsText.text = $"{fps} FPS";
    }
}
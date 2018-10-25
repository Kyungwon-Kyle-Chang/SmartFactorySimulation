using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedText : MonoBehaviour {

    private TextMeshProUGUI _tmproComp;
    private Coroutine _coroutine;

    private const float TARGET_TIME = 1.0f;


    private void Awake()
    {
        _tmproComp = GetComponent<TextMeshProUGUI>();
    }

    public void SetSpeedText(int speed)
    {
        _tmproComp.text = $"Play Speed : {speed}";

        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        Color baseColor = _tmproComp.color;

        baseColor.a = 1;
        _tmproComp.color = baseColor;
        yield return null;

        int fps = (int)(TARGET_TIME / Time.fixedDeltaTime);
        for (int i = 0; i < fps; i++)
        {
            baseColor.a -= 1.0f / fps;
            _tmproComp.color = baseColor;
            yield return null;
        }
    }
}

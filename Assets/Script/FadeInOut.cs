using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    private Image image;

    public void Fade_InOut(bool isIn, float fadeTime)
    {
        if (isIn)
            StartCoroutine(Fade(1f, 0f, 3f));
        else
            StartCoroutine(Fade(0f, 1f, 3f));
    }

    IEnumerator Fade(float start, float end, float fadeTime)
    {

        yield return null;

        fadeTime = Mathf.Clamp(fadeTime, 0.1f, 5f);

        float percent = 0f;
        float current = 0f;

        Color alpha = image.color;

        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            alpha.a = Mathf.Lerp(start, end, percent);
            image.color = alpha;
            yield return null;
        }

        if (end < 0.1f)
            image.raycastTarget = false;
        else
            image.raycastTarget = true;

    }
}

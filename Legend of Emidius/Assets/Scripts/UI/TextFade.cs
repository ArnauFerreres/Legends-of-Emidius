using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextFade : MonoBehaviour
{
    public float fadeSpeed = 0.5f;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 3f));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha)
    {
        float timer = 0f;

        while (timer < fadeSpeed)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeSpeed);
            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}






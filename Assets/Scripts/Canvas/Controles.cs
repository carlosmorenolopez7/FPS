using UnityEngine;
using TMPro;
using System.Collections;

public class Controles : MonoBehaviour
{
    public TextMeshProUGUI textToFade;

    void Start()
    {
        if (textToFade != null)
        {
            StartCoroutine(ShowAndFadeOutText(textToFade, 10f, 1f));
        }
    }

    private IEnumerator ShowAndFadeOutText(TextMeshProUGUI text, float displayDuration, float fadeDuration)
    {
        yield return new WaitForSeconds(displayDuration);
        float startAlpha = text.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        text.alpha = 0f;
    }
}
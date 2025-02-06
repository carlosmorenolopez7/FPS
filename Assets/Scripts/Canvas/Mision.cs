using UnityEngine;
using TMPro;
using System.Collections;

public class Mision : MonoBehaviour
{
    public TextMeshProUGUI misionText;

    void Start()
    {
        if (misionText != null)
        {
            misionText.gameObject.SetActive(false);
        }
    }

    public void ActivarCanvas()
    {
        if (misionText != null)
        {
            misionText.gameObject.SetActive(true);
            StartCoroutine(FadeInText(misionText));
        }
    }

    private IEnumerator FadeInText(TextMeshProUGUI text)
    {
        text.alpha = 0f;
        float duration = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            text.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        text.alpha = 1f;
    }
}
using UnityEngine;
using TMPro;
using System.Collections;

public class Misiones : MonoBehaviour
{
    public TextMeshProUGUI misionPrincipalText;
    public TextMeshProUGUI misionSecundariaText;
    private int totalEnemies;
    private int remainingEnemies;

    void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        remainingEnemies = totalEnemies;
        UpdateMisionSecundariaText();
    }

    void Update()
    {
        remainingEnemies = GameObject.FindGameObjectsWithTag("enemy").Length;
        UpdateMisionSecundariaText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("llave"))
        {
            StartCoroutine(ChangeColorWithTransition(misionPrincipalText, Color.green));
            Destroy(other.gameObject);
        }
    }

    private void UpdateMisionSecundariaText()
    {
        misionSecundariaText.text = $"Secundario: enemigos restantes {remainingEnemies}/{totalEnemies}";
        if (remainingEnemies == 0)
        {
            StartCoroutine(ChangeColorWithTransition(misionSecundariaText, Color.green));
        }
    }

    private IEnumerator ChangeColorWithTransition(TextMeshProUGUI text, Color targetColor)
    {
        Color initialColor = text.color;
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            yield return null;
        }

        text.color = targetColor;
    }
}
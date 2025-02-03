using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public static GameManager Instance {get; private set;}
    public int ammo = 10;
    public int health = 100;
    public Image gameOverImage;
    public float gameOverDelay = 3f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (health > 100)
        {
            health = 100;
        }
        ammoText.text = ammo.ToString();
        healthText.text = health.ToString();
    }

    public void LoseHealth(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Debug.Log("Game Over");
            GameOver();
        }
    }

    private void GameOver()
    {
        if (gameOverImage != null)
        {
            gameOverImage.gameObject.SetActive(true);
        }
        StartCoroutine(RestartGameAfterDelay(gameOverDelay));
    }

    private IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
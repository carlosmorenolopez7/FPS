using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public static GameManager Instance { get; private set; }
    public int ammo = 10;
    public int health = 100;
    public Image gameOverImage;
    public float gameOverDelay = 5f;
    public AudioSource musicaMapa;
    public GameObject objetoActivar;

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

    public void TransitionMusic()
    {
        StartCoroutine(TransitionMusicCoroutine());
    }

    private IEnumerator TransitionMusicCoroutine()
    {
        yield return StartCoroutine(FadeOutMusic(musicaMapa, 1f));
    }

    private IEnumerator FadeOutMusic(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

        public void ActivateObjectAfterDelay()
    {
        StartCoroutine(ActivateObjectCoroutine());
    }

    private IEnumerator ActivateObjectCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if (objetoActivar != null)
        {
            objetoActivar.SetActive(true);
        }
    }
}
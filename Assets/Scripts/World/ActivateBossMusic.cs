using UnityEngine;
using System.Collections;

public class ActivateBossMusic : MonoBehaviour
{
    public AudioSource bossMusic;
    public float fadeDuration = 1f;

    private void Start()
    {
        if (bossMusic != null)
        {
            bossMusic.volume = 0f;
            bossMusic.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeInMusic(bossMusic, fadeDuration));
        }
    }

    private IEnumerator FadeInMusic(AudioSource audioSource, float duration)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        float targetVolume = 1f;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}
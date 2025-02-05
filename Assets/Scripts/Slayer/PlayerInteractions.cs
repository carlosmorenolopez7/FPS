using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public AudioClip sonidoRecogidaMunicion;
    public AudioClip sonidoRecogidaVida;
    public AudioClip sonidoRecogidaLlave;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AmmoBox"))
        {
            GameManager.Instance.ammo += other.gameObject.GetComponent<AmmoBox>().ammo;
            PlaySound(sonidoRecogidaMunicion);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HealthContainer"))
        {
            GameManager.Instance.health += other.gameObject.GetComponent<HealthContainer>().health;
            PlaySound(sonidoRecogidaVida);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("llave"))
        {
            PlaySound(sonidoRecogidaLlave);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            GameManager.Instance.LoseHealth(5);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
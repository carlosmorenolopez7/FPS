using System;
using System.Collections;
using UnityEngine;

public class ControlBala : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("enemy"))
        {
            audioSource.Play();
            StartCoroutine(Destruir(other.gameObject));
        }
    }

    IEnumerator Destruir(GameObject other)
    {
        yield return new WaitForSeconds(1f);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
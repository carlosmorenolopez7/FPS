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

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cubo"))
        {
            audioSource.Play();
            StartCoroutine(Destruir(other.gameObject));
        }
    }*/

    IEnumerator Destruir(GameObject other)
    {
        yield return new WaitForSeconds(1f);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
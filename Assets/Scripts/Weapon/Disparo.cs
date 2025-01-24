using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Disparo : MonoBehaviour
{
    private bool puedoDisparar = true;


    public float[] fuerzaDisparo;
    public float[] tiempoDisparo;
    public GameObject[] armas;
    public Transform[] puntoDisparo;
    public GameObject[] balas;
    public AudioClip[] audio;

    private int indiceArma = 0;
    private PlayerMouse playerMouse;
    private AudioSource audioSource;


    void Start()
    {
        playerMouse = FindObjectOfType<PlayerMouse>();
        audioSource = GetComponent<AudioSource>();
        CambiarArmas(0);
    }

    private void CambiarArmas(int indiceArma)
    {
        for (int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(false);
        }

        armas[indiceArma].SetActive(true);
        audioSource.clip = audio[indiceArma];
    }

    public GameObject GetArma()
    {
        return armas[indiceArma];
    }

    public void OnNext(InputValue valor)
    {
        if (++indiceArma >= armas.Length)
            indiceArma = 0;
        CambiarArmas(indiceArma);
    }

    public void OnDisparar(InputValue valor)
    {
        if (puedoDisparar)
        {
            puedoDisparar = false;
            audioSource.Play();
            GameObject bala = Instantiate(balas[indiceArma], puntoDisparo[indiceArma].position,
                puntoDisparo[indiceArma].rotation);
            bala.GetComponent<Rigidbody>().AddForce(puntoDisparo[indiceArma].forward * fuerzaDisparo[indiceArma]);
            StartCoroutine(EsperaDisparo());
        }
    }

    IEnumerator EsperaDisparo()
    {
        yield return new WaitForSeconds(tiempoDisparo[indiceArma]);
        puedoDisparar = true;
    }
}
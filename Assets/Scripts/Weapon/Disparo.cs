using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Disparo : MonoBehaviour
{
    public GameObject[] armas;
    public float[] fuerzaDisparo;
    public float[] tiempoDisparo;
    public float[] damage;
    public int[] municionPorDisparo;
    private int indiceArma = 0;
    private bool puedoDisparar = true;
    private AudioSource audioSource;
    public Animator animator;
    public Camera fpsCam;
    public ParticleSystem[] muzzleFlashes;
    public GameObject impactEffect;
    public float range = 100;
    public float impactForce = 30;
    public AudioClip[] audioClips;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void CambiarArmas(int indice)
    {
        for (int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(false);
        }
        armas[indice].SetActive(true);
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
        animator.SetTrigger("CambioArma");
    }

    public void OnDisparar(InputValue valor)
    {
        if (puedoDisparar && GameManager.Instance.ammo >= municionPorDisparo[indiceArma])
        {
            GameManager.Instance.ammo -= municionPorDisparo[indiceArma];
            puedoDisparar = false;
            audioSource.PlayOneShot(audioClips[indiceArma]);

            if (muzzleFlashes[indiceArma] != null)
            {
                muzzleFlashes[indiceArma].Play();
            }

            RaycastHit hit;
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.red, 2f);
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log("Impacto en: " + hit.transform.name);
                if (hit.transform.CompareTag("enemy"))
                {
                    EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage((int)damage[indiceArma]);
                        Debug.Log("Daño aplicado al enemigo: " + hit.transform.name);
                    }
                }

                if (impactEffect != null)
                {
                    GameObject impactItem = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactItem, 2f);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
            StartCoroutine(EsperaDisparo());
        }
    }

    IEnumerator EsperaDisparo()
    {
        yield return new WaitForSeconds(tiempoDisparo[indiceArma]);
        puedoDisparar = true;
    }
}
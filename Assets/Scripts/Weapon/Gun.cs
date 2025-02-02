using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public float dmg = 20;
    public float range = 100;
    public float impactForce = 30;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    void Update()
    {     

    }

    public void OnDisparar(InputValue valor)
    {
        if (GameManager.Instance.ammo > 0)
        {
            muzzleFlash.Play();
            GameManager.Instance.ammo--;
            RaycastHit hit;

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range));
            {
                Debug.Log(hit.transform.name);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactItem = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactItem, 2f);
        }
    }
}

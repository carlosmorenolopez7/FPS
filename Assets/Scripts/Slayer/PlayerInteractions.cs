using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("AmmoBox")) 
        {
            GameManager.Instance.ammo += other.gameObject.GetComponent<AmmoBox>().ammo;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            GameManager.Instance.LoseHealth(5);
        }
    }
}

using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 1);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

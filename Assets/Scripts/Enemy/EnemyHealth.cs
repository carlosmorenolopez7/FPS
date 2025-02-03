using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int vida = 100;
    public GameObject explosionPrefab;
    public float explosionDelay = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {
        if (vida <= 0)
        {
            Explode();
        }
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject, explosionDelay);
    }
}
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int vida = 100;

    void Start()
    {

    }

    void Update()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
    }
}
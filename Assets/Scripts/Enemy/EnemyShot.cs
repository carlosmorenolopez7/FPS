using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform bulletPoint;
    private Transform playerPosition;
    public float bulletForce = 100;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = FindObjectOfType<PlayerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        Vector3 playerDirection = playerPosition.position - transform.position;
        GameObject newBullet;
        newBullet = Instantiate(enemyBullet, bulletPoint.position, bulletPoint.rotation);
    }
}

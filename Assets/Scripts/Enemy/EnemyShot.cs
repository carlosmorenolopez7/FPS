using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform bulletPoint;
    private Transform playerPosition;
    public float bulletForce = 100;
    public AudioClip audioClip;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioClip = GetComponent<AudioClip>();
        playerPosition = FindObjectOfType<PlayerMove>().transform;
        Invoke("ShootPlayer",3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootPlayer()
    {
        GetComponent<AudioSource>().Play();
        Vector3 playerDirection = playerPosition.position - transform.position;
        GameObject newBullet;
        newBullet = Instantiate(enemyBullet, bulletPoint.position, bulletPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(playerDirection * bulletForce, ForceMode.Impulse);
        Invoke("ShootPlayer", 3);
    }
}

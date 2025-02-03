using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyShot : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform bulletPoint;
    private Transform playerPosition;
    public float bulletForce = 100;
    ///
    public AudioClip audioClip;
    public Transform path;
    public float visionAngle = 45f;
    public float visionDistance = 10f;
    public Transform player;
    public float distanceThreshold = 2f;
    public float searchSpeedMultiplier = 2.5f;
    public float searchRadius = 20f;
    public float waypointWaitTime = 3f;
    public float minDistanceFromWall = 5f;

    private UnityEngine.AI.NavMeshAgent agent;
    private Transform[] waypoints;
    private int childrenIndex = 0;
    private bool isChasingPlayer = false;
    private Vector3[] searchWaypoints;

    void Start()
    {
        audioClip = GetComponent<AudioClip>();
        playerPosition = FindObjectOfType<PlayerMove>().transform;
        Invoke("ShootPlayer",3);

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        waypoints = new Transform[path.childCount];
        for (int i = 0; i < path.childCount; i++)
        {
            waypoints[i] = path.GetChild(i);
        }

        SetDestinationToNextWaypoint();
    }

    void Update()
    {
        if (isChasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            MoveAlongPath();
            DetectPlayer();
        }
    }

    private void MoveAlongPath()
    {
        if (Vector3.Distance(transform.position, waypoints[childrenIndex].position) < distanceThreshold)
        {
            childrenIndex = (childrenIndex + 1) % waypoints.Length;
            SetDestinationToNextWaypoint();
        }
    }

    private void SetDestinationToNextWaypoint()
    {
        agent.SetDestination(waypoints[childrenIndex].position);
    }

    private void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (angle < visionAngle / 2f && Vector3.Distance(transform.position, player.position) < visionDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, visionDistance))
            {
                if (hit.transform == player)
                {
                    isChasingPlayer = true;
                }
            }
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (angle > visionAngle / 2f || Vector3.Distance(transform.position, player.position) > visionDistance)
        {
            isChasingPlayer = false;
            SetDestinationToNextWaypoint();
        }
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

    /*    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    }*/
}

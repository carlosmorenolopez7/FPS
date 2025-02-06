using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyShot : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform bulletPoint;
    private Transform playerPosition;
    public float bulletForce = 100f;
    public AudioClip audioClip;
    public float shootingDelay = 0.5f;
    public float visionAngle = 60f;
    public float visionDistance = 20f;
    public float circularDetectionRadius = 10f;
    public Transform player;
    public Transform path;
    public float distanceThreshold = 2f;
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform[] waypoints;
    private int childrenIndex = 0;
    private bool playerInSight = false;
    private AudioSource audioSource;
    public float shootVolume = 0.5f;

    void Start()
    {
        playerPosition = FindObjectOfType<PlayerMove>().transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        waypoints = new Transform[path.childCount];
        
        for (int i = 0; i < path.childCount; i++)
        {
            waypoints[i] = path.GetChild(i);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = shootVolume;
        }

        SetDestinationToNextWaypoint();
        InvokeRepeating("TryShootPlayer", shootingDelay, shootingDelay);
    }

    void Update()
    {
        DetectPlayer();
        FollowPlayerWithEyes();

        if (!playerInSight)
        {
            MoveAlongPath();
        }
    }

    private void SetDestinationToNextWaypoint()
    {
        agent.SetDestination(waypoints[childrenIndex].position);
    }

    private void MoveAlongPath()
    {
        if (Vector3.Distance(transform.position, waypoints[childrenIndex].position) < distanceThreshold)
        {
            childrenIndex = (childrenIndex + 1) % waypoints.Length;
            SetDestinationToNextWaypoint();
        }
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
                    playerInSight = true;
                    //Debug.Log("Jugador detectado (cono de visión)");
                }
                else
                {
                    playerInSight = false;
                }
            }
        }
        else
        {
            playerInSight = false;
        }

        if (!playerInSight && Vector3.Distance(transform.position, player.position) < circularDetectionRadius)
        {
            playerInSight = true;
            //Debug.Log("Jugador detectado (circular)");
        }
    }

    private void FollowPlayerWithEyes()
    {
        if (playerInSight)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void TryShootPlayer()
    {
        if (playerInSight)
        {
            ShootPlayer();
        }
    }

    void ShootPlayer()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        Vector3 playerDirection = playerPosition.position - transform.position;
        GameObject newBullet = Instantiate(enemyBullet, bulletPoint.position, bulletPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(playerDirection.normalized * bulletForce, ForceMode.Impulse);
    }
}
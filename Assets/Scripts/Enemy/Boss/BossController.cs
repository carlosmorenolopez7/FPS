using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform[] shootingBulletPoints;
    public Transform[] staticBulletPoints;
    public float bulletForce = 100f;
    public AudioClip audioClip;
    public float shootingDelay = 0.5f;
    public float visionAngle = 60f;
    public float visionDistance = 20f;
    public float circularDetectionRadius = 10f;
    public Transform player;
    public Transform path;
    public float distanceThreshold = 2f;
    private NavMeshAgent agent;
    private Transform[] waypoints;
    private int childrenIndex = 0;
    private bool playerInSight = false;
    private Animator animator;
    private AudioSource audioSource;
    public float shootVolume = 0.2f;

    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
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
        MoveAlongPath();
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
                    Debug.Log("Jugador detectado (cono de visión)");
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
            Debug.Log("Jugador detectado (circular)");
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
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shooting"))
            {
                ShootFromBulletPoints(shootingBulletPoints);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Static"))
            {
                ShootFromBulletPoints(staticBulletPoints);
            }
        }
    }

    void ShootFromBulletPoints(Transform[] bulletPoints)
    {
        foreach (Transform bulletPoint in bulletPoints)
        {
            Shoot(bulletPoint);
        }
    }

    void Shoot(Transform bulletPoint)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip);
        Vector3 playerDirection = player.position - bulletPoint.position;
        GameObject newBullet = Instantiate(enemyBullet, bulletPoint.position, bulletPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(playerDirection.normalized * bulletForce, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ActivateObjectAfterDelay();
        }
    }
}
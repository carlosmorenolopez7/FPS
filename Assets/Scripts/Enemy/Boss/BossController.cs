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

        SetDestinationToNextWaypoint();
        InvokeRepeating("TryShootPlayer", shootingDelay, shootingDelay); // Intentar disparar a intervalos
    }

    void Update()
    {
        DetectPlayer();
        FollowPlayerWithEyes();

        // Movimiento continuo entre waypoints, siempre
        MoveAlongPath();
    }

    private void SetDestinationToNextWaypoint()
    {
        agent.SetDestination(waypoints[childrenIndex].position);
    }

    private void MoveAlongPath()
    {
        // Mover al siguiente waypoint solo cuando llegue al actual
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
                ShootFromBulletPoints(shootingBulletPoints); // Disparar desde puntos de Shooting
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Static"))
            {
                ShootFromBulletPoints(staticBulletPoints); // Disparar desde puntos de Static
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
        GetComponent<AudioSource>().PlayOneShot(audioClip); // Reproducir sonido de disparo
        Vector3 playerDirection = player.position - bulletPoint.position;
        GameObject newBullet = Instantiate(enemyBullet, bulletPoint.position, bulletPoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(playerDirection.normalized * bulletForce, ForceMode.Impulse);
    }
}
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public Light patrolLight;
    public float viewAngle = 45.0f;
    public float viewDistance = 10.0f;
    public LayerMask viewMask;
    public Animator animator;
    public float patrolRadius = 10.0f;
    public float runAwayDistance = 15.0f; // Distance to run away from the player

    private Color originalLightColor;
    private bool isAlerted = false;
    public GameObject caughtPanel;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Find the player by tag
       // caughtPanel = GameObject.Find("CaughtPanel"); // Find the CaughtPanel GameObject
        if (caughtPanel == null)
        {
            Debug.LogError("CaughtPanel GameObject not found in the scene.");
        }
        else
        {
            caughtPanel.SetActive(false); // Initially disable the CaughtPanel
        }

        dododod();
    }

    void dododod()
    {
        originalLightColor = patrolLight.color;
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        SetRandomDestination();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            isAlerted = true;
            patrolLight.color = Color.yellow; // Change light color to yellow
            RunAwayFromPlayer(); // NPC runs away from the player
            if (caughtPanel != null)
            {
                caughtPanel.SetActive(true); // Activate the CaughtPanel canvas
            }
        }
        else
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                SetRandomDestination(); // Keep moving randomly
                animator.SetTrigger("isIdle"); // Set to idle while randomly walking
                if (isAlerted)
                {
                    patrolLight.color = originalLightColor; // Reset light color
                    isAlerted = false;
                    if (caughtPanel != null)
                    {
                        caughtPanel.SetActive(false); // Deactivate the CaughtPanel canvas
                    }
                }
            }
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1))
        {
            finalPosition = hit.position;
        }

        navMeshAgent.SetDestination(finalPosition);
        navMeshAgent.isStopped = false;
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleBetweenGuardAndPlayer < viewAngle / 2 && Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            if (!Physics.Linecast(transform.position, player.position, viewMask))
            {
                return true; // Player is visible
            }
        }
        return false; // Player is not visible
    }

    void RunAwayFromPlayer()
    {
        Vector3 directionAwayFromPlayer = transform.position - player.position;
        Vector3 runAwayTarget = transform.position + directionAwayFromPlayer.normalized * runAwayDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(runAwayTarget, out hit, runAwayDistance, 1))
        {
            navMeshAgent.SetDestination(hit.position);
        }
    }
}

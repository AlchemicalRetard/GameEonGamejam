using UnityEngine;
using UnityEngine.AI;

public class AlertNPC : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public Light patrolLight;
    public float viewAngle = 45.0f;
    public float viewDistance = 10.0f;
    public LayerMask viewMask;
    public Animator animator; // Animator reference
    public float patrolRadius = 10.0f; // Radius for random patrol

    private bool isAlerted = false;
    private Color originalLightColor;
    private float timeSinceLastSeenPlayer = 0f; // Timer
    private const float timeToReset = 5f; // Time to reset to normal behavior

    void Start()
    {
        if (patrolLight == null)
        {
            patrolLight = GetComponentInChildren<Light>();
        }
        originalLightColor = patrolLight.color;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        SetRandomDestination();
    }

    void Update()
    {
        if (isAlerted)
        {
            if (CanSeePlayer())
            {
                timeSinceLastSeenPlayer = 0; // Reset timer if player is seen
                animator.SetTrigger("LookAround"); // Player spotted, trigger LookAround
            }
            else
            {
                timeSinceLastSeenPlayer += Time.deltaTime;
                if (timeSinceLastSeenPlayer >= timeToReset)
                {
                    isAlerted = false;
                    patrolLight.color = originalLightColor; // Reset light color
                    SetRandomDestination(); // Resume normal patrol
                    navMeshAgent.isStopped = false;
                    animator.SetTrigger("isIdle"); // Set to idle after not seeing player
                }
            }

        }
        else
        {
            if (CanSeePlayer())
            {
                isAlerted = true;
                patrolLight.color = Color.yellow; // Change light color to yellow
                navMeshAgent.isStopped = true; // Stop the NPC
            }
            else if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                SetRandomDestination(); // Keep moving randomly
                animator.SetTrigger("isIdle"); // Set to idle while randomly walking
            }
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
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
}

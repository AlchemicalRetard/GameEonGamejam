/*using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic; // For handling collections like List

public class AlertResponseNPC : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public Light patrolLight;
    public float viewAngle = 45.0f;
    public float viewDistance = 10.0f;
    public LayerMask viewMask;
    public Animator animator; // Animator reference
    public float patrolRadius = 10.0f; // Radius for random patrol
    public float runAwayDistance = 15.0f;

    private bool goToSpecificPoint = false;
    private Vector3 specificPoint;
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
    public void UpdatePosition(Vector3 newPosition)
    {
        Debug.Log("Done");
        transform.position = newPosition;
    }
    void Update()
    {
        if (isAlerted)
        {
            if (CanSeePlayer())
            {
                RunAwayFromPlayer(); // NPC runs away from the player
                player.GetComponent<PlayerMovement>().GetStunned(); // Stun the player
                timeSinceLastSeenPlayer = 0;
                animator.SetTrigger("LookAround");
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

        // Check if NPC should go to a specific point
        if (goToSpecificPoint)
        {
            if (Vector3.Distance(transform.position, specificPoint) < navMeshAgent.stoppingDistance)
            {
                goToSpecificPoint = false; // Reset flag
                isAlerted = true; // Start looking for the player
                patrolLight.color = Color.yellow; // Change light color to indicate alertness
            }
        }

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
    public void SetDestination(Vector3 targetPosition)
    {
        specificPoint = targetPosition;
        goToSpecificPoint = true;
        navMeshAgent.SetDestination(specificPoint);
        navMeshAgent.isStopped = false;
    }

}
*/

using UnityEngine;
using UnityEngine.AI;

public class AlertResponseNPC : MonoBehaviour
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

    void Start()
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
            player.GetComponent<PlayerMovement>().GetStunned(); // Stun the player
            RunAwayFromPlayer(); // NPC runs away from the player
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

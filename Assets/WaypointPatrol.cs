/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player; // Reference to the player
    public float patrolRadius = 10.0f;
    public float viewAngle = 45.0f; // View angle for detecting the player
    public float viewDistance = 10.0f; // View distance for detecting the player
    public LayerMask viewMask; // LayerMask for the player and obstacles

    private bool isChasing = false;

    void Start()
    {
        SetRandomDestination();
    }

    void Update()
    {

      

        if (isChasing)
        {
            // Chase the player
            navMeshAgent.SetDestination(player.position);
            if (!CanSeePlayer())
            {
                isChasing = false; // Stop chasing if player is not visible
                SetRandomDestination(); // Return to patrol
            }
        }
        else
        {
            // Patrol logic
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                SetRandomDestination();
            }

            if (CanSeePlayer())
            {
                isChasing = true; // Start chasing if player is visible
            }
        }
    }
    bool IsPlayerIlluminated()
    {
        Light[] lights = FindObjectsOfType<Light>(); // Find all lights in the scene
        foreach (Light light in lights)
        {
            float distanceToPlayer = Vector3.Distance(light.transform.position, player.position);
            if (distanceToPlayer < light.range) // Check if player is within light range
            {
                Vector3 dirToPlayer = (player.position - light.transform.position).normalized;
                if (Vector3.Angle(light.transform.forward, dirToPlayer) < light.spotAngle / 2) // Check for spotlights
                {
                    RaycastHit hit;
                    if (Physics.Raycast(light.transform.position, dirToPlayer, out hit, light.range))
                    {
                        if (hit.collider.transform == player)
                        {
                            return true; // Player is illuminated and not obstructed
                        }
                    }
                }
            }
        }
        return false; // Player is not illuminated by any light
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
    }
    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleBetweenGuardAndPlayer < viewAngle / 2 && Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            // Debug line (Green if player is in view angle and distance, Red otherwise)
            Debug.DrawLine(transform.position, player.position, Color.green);

            if (!Physics.Linecast(transform.position, player.position, viewMask))
            {
                return true; // Player is visible
            }
            else
            {
                // Draw line of sight blocked (Yellow)
                Debug.DrawLine(transform.position, player.position, Color.yellow);
            }
        }
        else
        {
            // Draw line of sight out of range/angle (Red)
            Debug.DrawLine(transform.position, player.position, Color.red);
        }

        return false; // Player is not visible
    }

   
}
*/

/*using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player; // Reference to the player
    public Light patrolLight; // Reference to the NPC's light
    public float viewAngle = 45.0f; // View angle for detecting the player
    public float viewDistance = 10.0f; // View distance for detecting the player
    public LayerMask viewMask; // LayerMask for the player and obstacles

    private bool isChasing = false;
    private Color originalLightColor;

    public Animator anim;

    void Start()
    {
        if (patrolLight == null)
        {
            patrolLight = GetComponentInChildren<Light>();
        }
        originalLightColor = patrolLight.color;
        SetRandomDestination();
    }

    void Update()
    {
        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position);
            if (!CanSeePlayer())
            {
                isChasing = false;
                patrolLight.color = originalLightColor; // Change light color back
                SetRandomDestination();
            }
        }
        else
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                SetRandomDestination();
            }

            if (CanSeePlayer())
            {
                isChasing = true;
                patrolLight.color = Color.red; // Change light color to red
            }
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.onUnitSphere * 4f; // 4 units in a random direction
        randomDirection += transform.position;
        randomDirection.y = transform.position.y; // Keep the y coordinate the same

        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, 4f, 1))
        {
            finalPosition = hit.position;
        }

        navMeshAgent.SetDestination(finalPosition);
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
}*/



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
    public Animator animator; // Animator reference

    private bool isChasing = false;
    private Color originalLightColor;

    void Start()
    {
        if (patrolLight == null)
        {
            patrolLight = GetComponentInChildren<Light>();
        }
        originalLightColor = patrolLight.color;
        SetRandomDestination();

        // Initialize the Animator reference
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position);
            patrolLight.color = Color.red; // Change light color to red
            if (!CanSeePlayer())
            {
                isChasing = false;
                patrolLight.color = originalLightColor; // Change light color back
                SetRandomDestination();
                animator.SetTrigger("isIdle"); // Set the Idle trigger
            }
        }
        else
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                SetRandomDestination();
                animator.SetTrigger("isIdle"); // Set the Idle trigger
            }

            if (CanSeePlayer())
            {
                isChasing = true;
                patrolLight.color = Color.red; // Change light color to red
                animator.SetTrigger("isWalking"); // Set the isWalking trigger
            }
        }
    }

    // ... Rest of your methods (SetRandomDestination, CanSeePlayer)
    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.onUnitSphere * 4f; // 4 units in a random direction
        randomDirection += transform.position;
        randomDirection.y = transform.position.y; // Keep the y coordinate the same

        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, 4f, 1))
        {
            finalPosition = hit.position;
        }

        navMeshAgent.SetDestination(finalPosition);
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

using UnityEngine;

public class AnxietyTablet : MonoBehaviour
{
    public float anxietyReductionAmount = 10f; // Amount by which the anxiety is reduced
    public float cooldownReductionAmount = 5f; // Amount by which ability cooldown is reduced

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("touched");
            // Directly access the TimeSlowerAbility script attached to the player
            TimeSlower timeSlower = other.GetComponent<TimeSlower>();
            PlayerMovement playerScript = other.GetComponent<PlayerMovement>(); // Assuming you have a Player script for anxiety management

            if (timeSlower != null)
            {
                // Reduce the cooldown of the ability
                timeSlower.ReduceCooldown(cooldownReductionAmount);
            }

            if (playerScript != null)
            {
                anxietyMeter playerAnxiety = other.GetComponent<anxietyMeter>();
                if (playerAnxiety != null)
                {
                    playerAnxiety.DecreaseAnxiety();
                }
            }


            // Optionally, destroy the tablet object after collecting
            Destroy(gameObject);
        }
    }
}

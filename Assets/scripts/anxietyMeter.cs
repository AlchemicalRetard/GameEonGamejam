using UnityEngine;
using UnityEngine.UI; // If you're using UI elements

public class anxietyMeter : MonoBehaviour
{
    public Slider anxietyBar; // UI Slider to represent the anxiety meter
    private float maxAnxiety = 100f; // Maximum anxiety level
    private float currentAnxiety; // Current anxiety level
    private float anxietyDecreaseRate = 10f; // Rate at which anxiety decreases when tablets are collected

    void Start()
    {
        // Initialize the anxiety meter
        currentAnxiety = 0f; // Starting anxiety level
        anxietyBar.maxValue = maxAnxiety;
        anxietyBar.value = currentAnxiety;
    }

    void Update()
    {
        // Update anxiety meter over time or based on game events
        IncreaseAnxietyOverTime(); // Increase anxiety gradually

        // Implement what happens if anxiety reaches max level
        if (currentAnxiety >= maxAnxiety)
        {
            // Trigger game over or other effects
            HandleMaxAnxiety();
        }
    }

    public void IncreaseAnxiety(float amount)
    {
        // Increase anxiety by a specified amount
        currentAnxiety += amount;
        currentAnxiety = Mathf.Clamp(currentAnxiety, 0, maxAnxiety);
        anxietyBar.value = currentAnxiety;
    }

    public void DecreaseAnxiety()
    {
        // Decrease anxiety when a tablet is collected
        currentAnxiety -= anxietyDecreaseRate;
        currentAnxiety = Mathf.Clamp(currentAnxiety, 0, maxAnxiety);
        anxietyBar.value = currentAnxiety;
    }

    private void IncreaseAnxietyOverTime()
    {
        // Gradually increase anxiety over time
        // Adjust the rate as needed
        currentAnxiety += Time.deltaTime;
        currentAnxiety = Mathf.Clamp(currentAnxiety, 0, maxAnxiety);
        anxietyBar.value = currentAnxiety;
    }

    private void HandleMaxAnxiety()
    {
        // Handle the situation when anxiety reaches the maximum level
        Debug.Log("Anxiety reached max level! Game Over!");
        // Implement game over logic or other consequences
    }
}

/*
using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using System.Collections;

public class TimeSlower : MonoBehaviour
{
    public float slowMotionFactor = 0.5f;
    public float abilityDuration = 5f;
    public float cooldownDuration = 20f;

    private float cooldownTimer = 0f;

    public Slider cooldownSlider; // Reference to a UI Slider component

    void Start()
    {
        if (cooldownSlider != null)
        {
            cooldownSlider.maxValue = cooldownDuration;
            cooldownSlider.value = cooldownDuration; // Initialize the slider
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && cooldownTimer <= 0)
        {
            StartCoroutine(ActivateAbility());
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.unscaledDeltaTime;
            UpdateCooldownSlider(); // Update the UI slider
        }
        else if (cooldownSlider != null)
        {
            cooldownSlider.value = cooldownSlider.maxValue; // Reset the slider when cooldown is over
        }
    }

    private IEnumerator ActivateAbility()
    {
        float originalFixedDeltaTime = Time.fixedDeltaTime;

        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowMotionFactor;

        yield return new WaitForSecondsRealtime(abilityDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowMotionFactor;

        cooldownTimer = cooldownDuration;
    }

    private void UpdateCooldownSlider()
    {
        if (cooldownSlider != null)
        {
            // Update the slider value based on the remaining cooldown time
            cooldownSlider.value = cooldownDuration - cooldownTimer;
        }
    }

    public void ReduceCooldown(float amount)
    {
        cooldownTimer -= amount;
        if (cooldownTimer < 0) cooldownTimer = 0;
    }
}
*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TimeSlower : MonoBehaviour
{
    public float slowMotionFactor = 0.5f;
    public float abilityDuration = 5f;
    public float cooldownDuration = 20f;

    private float cooldownTimer = 0f;

    // New UI components
    public Image cooldownImage;
    public TextMeshProUGUI cooldownText;

    void Start()
    {
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0;
        }

        if (cooldownText != null)
        {
            cooldownText.text = "";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && cooldownTimer <= 0)
        {
            cooldownTimer = cooldownDuration; // Start cooldown immediately
            StartCoroutine(ActivateAbility());
        }

        AbilityCooldown();
    }

    private IEnumerator ActivateAbility()
    {
        float originalFixedDeltaTime = Time.fixedDeltaTime;

        // Adjust time scale for ability effect
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowMotionFactor;

        // Wait for ability duration
        yield return new WaitForSecondsRealtime(abilityDuration);

        // Reset time scale back to normal
        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
    public void ReduceCooldown(float amount)
    {
        cooldownTimer -= amount;
        if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }

        // Update the UI components if they exist
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = cooldownTimer / cooldownDuration;
        }

        if (cooldownText != null)
        {
            cooldownText.text = cooldownTimer > 0 ? Mathf.Ceil(cooldownTimer).ToString() : "";
        }
    }

    private void AbilityCooldown()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.unscaledDeltaTime;

            // Update the UI components
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = cooldownTimer / cooldownDuration;
            }

            if (cooldownText != null)
            {
                cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();
            }
        }
        else
        {
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = 0f;
            }

            if (cooldownText != null)
            {
                cooldownText.text = "";
            }
        }

    }

} 
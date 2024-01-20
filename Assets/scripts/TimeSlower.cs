/*using UnityEngine;

public class TimeSlower : MonoBehaviour
{
    public float slowDownFactor = 0.5f; // How much to slow down time
    public float slowDownLength = 5f; // Duration of the slow down effect

    private bool isSlowingTime = false;

    void Update()
    {
        // Check for player input to activate the ability
        if (Input.GetKeyDown(KeyCode.T)) // 'T' key as an example
        {
            StartSlowMotion();
        }

        // Gradually return to normal time
        if (isSlowingTime && Time.timeScale < 1f)
        {
            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }

    void StartSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f; // Adjust physics update to match slow motion
        isSlowingTime = true;
        Invoke("ResetTimeScale", slowDownLength);
    }

    void ResetTimeScale()
    {
        isSlowingTime = false;
    }
}
*/
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

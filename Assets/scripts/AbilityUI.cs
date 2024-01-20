using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    [Header("StopTime")]
    public Image dashImage;
    public TextMeshProUGUI dashText;
    public KeyCode dashKey;
    public float dashCooldown = 10;

   /* [Header("Invinsibility")]
    public Image InvinsibilityImage;
    public TextMeshProUGUI InvinsibilityText;
    public KeyCode InvinsibilityKey;
    public float InvinsibilityCooldown = 10;

    [Header("Stomp")]
    public Image StompImage;
    public TextMeshProUGUI StompText;
    public KeyCode StompKey;
    public float StompCooldown = 10;*/


    private bool isDashCooldown = false;
    private bool isInvinsibilityCooldown = false;
    private bool isStompCooldown = false;

    private float currentDashCooldown;
    private float currentInvinsibilityCooldown;
    private float currentStompCooldown;

    // Start is called before the first frame update
    void Start()
    {
        dashImage.fillAmount = 0;
        // InvinsibilityImage.fillAmount = 0;
        // StompImage.fillAmount = 0;

        dashText.text = "";
        // StompText.text = "";
        // InvinsibilityText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        DashInput();
        //InvinsibilityInput();
        //StompInput();

        AbilityCooldown(ref currentDashCooldown, dashCooldown, ref isDashCooldown, dashImage, dashText);
       // AbilityCooldown(ref currentInvinsibilityCooldown, InvinsibilityCooldown, ref isInvinsibilityCooldown, InvinsibilityImage, InvinsibilityText);
       // AbilityCooldown(ref currentStompCooldown, StompCooldown, ref isStompCooldown, StompImage, StompText);
    }

    void DashInput()
    {
        if (Input.GetKeyDown(dashKey) && !isDashCooldown)
        {
            isDashCooldown = true;
            currentDashCooldown = dashCooldown;
        }
    }

   /* void InvinsibilityInput()
    {
        if (Input.GetKeyDown(InvinsibilityKey) && !isInvinsibilityCooldown)
        {
            isInvinsibilityCooldown = true;
            currentInvinsibilityCooldown = InvinsibilityCooldown;
        }
    }

    void StompInput()
    {
        if (Input.GetKeyDown(StompKey) && !isStompCooldown)
        {
            isStompCooldown = true;
            currentStompCooldown = StompCooldown;
        }
    }*/

    void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, TextMeshProUGUI skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if (skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }
                if (skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if (skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }
            }
        }
    }
}
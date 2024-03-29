using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 40f;
    public float moveSpeed = 3f;

    private anxietyMeter anxiety;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        anxiety = FindObjectOfType<anxietyMeter>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.unscaledDeltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement *moveSpeed* Time.unscaledDeltaTime);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    public void reduction()   // used to refence anxiety from anxiety meter script
    {
        anxiety.DecreaseAnxiety();

    }


    public void GetStunned()
    {
        StartCoroutine(StunPlayer());
    }
    private IEnumerator StunPlayer()
    {
        float espeed = 5f;
        // Disable player movement
        m_Animator.SetBool("stuned",true);
        moveSpeed = 0;

        // Wait for 4 seconds
        yield return new WaitForSeconds(4);
        m_Animator.SetBool("stuned", false);
        // Re-enable player movement
        moveSpeed = espeed;
    }


}
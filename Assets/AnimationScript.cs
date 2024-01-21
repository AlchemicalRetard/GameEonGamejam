using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator animator;
    public float normalScreenTime = 1.2f;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(GetNormal());
    }
    private void Awake()
    {
        animator.SetTrigger("Entry");

    }
    IEnumerator GetNormal()
    {
        yield return new WaitForSeconds(normalScreenTime);
        animator.SetTrigger("idle");
    }
}

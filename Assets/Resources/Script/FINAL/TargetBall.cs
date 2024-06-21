using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBall : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        animator.Play("1");
        animator.Play("2");
        animator.Play("3");
        animator.Play("4");
        animator.Play("5");
        animator.Play("6");
        animator.Play("7");
        animator.Play("8");
        animator.Play("9");
        animator.Play("10");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving"); // 문자를 특정한 숫자 값으로 변환하여 비교
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsSliding = Animator.StringToHash("IsSliding");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsInvincivility = Animator.StringToHash("IsInvincivility");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move()
    {
        animator.SetBool(IsMoving, true);
    }

    public void SetJump(bool on)
    {
        if (on)
        {
            animator.SetBool(IsJumping, true);
            animator.SetBool(IsMoving, false);
        }
        else if (!on)
        {
            animator.SetBool(IsJumping, false);
            animator.SetBool(IsMoving, true);
        }
    }

    public void Slide()
    {
        animator.SetBool(IsSliding, true);
    }

    public void Stand()
    {
        animator.SetBool(IsSliding, false);
    }

    public void Damage()
    {
        animator.SetTrigger(IsDamage);
    }

    public void Invincibility()
    {
        animator.SetBool(IsInvincivility, true);
    }
}

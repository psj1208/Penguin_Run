using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove"); // 문자를 특정한 숫자 값으로 변환하여 비교
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsSliding = Animator.StringToHash("IsSliding");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move()
    {
        animator.SetBool(IsMoving, true);
    }

    public void Jump()
    {
        animator.SetBool(IsJumping, true);
    }

    public void Slide()
    {
        animator.SetBool(IsSliding, true);
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }
}

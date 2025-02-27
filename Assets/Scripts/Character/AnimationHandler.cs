using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // 애니메이터 파라미터 이름을 해시값으로 변환 (문자열 비교 비용 절감)
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsSliding = Animator.StringToHash("IsSliding");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsInvincibility = Animator.StringToHash("IsInvincibility");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // 이동 애니메이션
    public void Move()
    {
        animator.SetBool(IsMoving, true);
    }

    /// <summary>
    /// 점프 애니메이션
    /// true면 점프 애니메이션을 활성화하고 이동 애니메이션을 비활성화
    /// false면 점프 애니메이션을 비활성화하고 이동 애니메이션을 활성화
    /// </summary>
    /// <param name="ju">점프 여부</param>
    public void SetJump(bool ju)
    {
        if (ju)
        {
            animator.SetBool(IsJumping, true);
            animator.SetBool(IsMoving, false);
        }
        else
        {
            animator.SetBool(IsJumping, false);
            animator.SetBool(IsMoving, true);
        }
    }

    /// <summary>
    /// 슬라이딩 애니메이션
    /// true이면 슬라이딩 애니메이션을 활성화하고 false이면 비활성화
    /// </summary>
    /// <param name="sl">슬라이딩 여부</param>
    public void Slide(bool sl)
    {
        animator.SetBool(IsSliding, sl);
    }

    // 데미지 애니메이션
    public void Damage()
    {
        animator.SetTrigger(IsDamage);
    }

    /// <summary>
    /// 무적 상태 애니메이션
    /// true면 무적 애니메이션 활성화, false면 일반 상태
    /// </summary>
    /// <param name="inv">무적 상태 여부</param>
    public void Invincibility(bool inv)
    {
        animator.SetBool(IsInvincibility, inv);
    }
}

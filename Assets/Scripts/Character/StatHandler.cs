using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    // 플레이어 컨트롤러 및 애니메이션 핸들러 참조
    private PlayerController player;
    private AnimationHandler animationHandler;

    // 체력 및 속도 관련 변수
    [SerializeField, Range(0f, 100f)] private float hp;      // 현재 체력
    public float Hp => hp;

    [SerializeField, Range(0f, 100f)] private float maxHp;   // 최대 체력
    public float MaxHp => maxHp;

    [SerializeField, Range(0f, 100f)] private float speed;   // 현재 이동 속도
    public float Speed => speed;

    private float decreaseHPRatio; // 체력 자연 감소 비율

    // 무적 상태 관련 변수
    private float invincibilityTime;          // 무적 지속 시간
    private float invincibilityDurationTime;  // 무적 경과 시간
    [SerializeField] private bool isInvincibility; // 무적 여부

    private void Awake()
    {
        // 초기값 설정
        decreaseHPRatio = 1f;      // 초당 체력 감소량
        invincibilityTime = 2f;    // 무적 지속 시간
        invincibilityDurationTime = 0f;
        isInvincibility = false;

        // 컴포넌트 가져오기
        player = GetComponent<PlayerController>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Update()
    {
        // 체력 자연 감소
        hp -= decreaseHPRatio * Time.deltaTime;

        // 체력이 0 이하가 되면 게임 오버 처리
        if (hp <= 0)
        {
            GameManager.Instance.GameOver();
            return;
        }

        // 무적 상태 시간 확인
        if (isInvincibility)
        {
            invincibilityDurationTime += Time.deltaTime;
            if (invincibilityDurationTime >= invincibilityTime)
            {
                isInvincibility = false;
                invincibilityDurationTime = 0f;
            }
        }
    }

    /// <summary>
    /// 체력을 변경하는 함수
    /// 양수 값이면 회복, 음수 값이면 데미지 처리
    /// </summary>
    /// <param name="figure">변경할 체력 값</param>
    public void ChangeHP(float figure)
    {
        if (figure > 0f)
        {
            Heal(figure);
        }
        else
        {
            Damage(figure);
        }
    }

    /// <summary>
    /// 체력을 회복하는 함수
    /// </summary>
    /// <param name="figure">회복할 체력량</param>
    private void Heal(float figure)
    {
        hp += figure;
        hp = hp >= maxHp ? maxHp : hp; // 최대 체력을 초과하지 않도록 설정
    }

    /// <summary>
    /// 체력 감소(데미지 처리) 함수
    /// 무적 상태가 아닐 경우만 적용됨
    /// </summary>
    /// <param name="figure">감소할 체력량 (음수 값)</param>
    private void Damage(float figure)
    {
        if (!isInvincibility)
        {
            isInvincibility = true;
            hp += figure; // figure가 음수이므로 실제로는 체력이 감소함
            animationHandler.Damage(); // 피격 애니메이션 재생
        }
    }

    /// <summary>
    /// 속도를 변경하는 함수
    /// 양수 값이면 부스터 효과 적용
    /// </summary>
    /// <param name="amount">속도 변경 값</param>
    /// <param name="duration">지속 시간 (초)</param>
    public void ChangeSpeed(int amount, int duration)
    {
        if (amount > 0)
        {
            Booster(amount, duration);
        }
    }

    /// <summary>
    /// 부스터 효과 적용 (일정 시간 동안 속도 증가)
    /// </summary>
    /// <param name="amount">추가할 속도 값</param>
    /// <param name="duration">지속 시간 (초)</param>
    public void Booster(int amount, int duration)
    {
        if (amount > 0)
        {
            isInvincibility = true; // 부스터 중에는 무적 상태
            animationHandler.Invincibility(); // 무적 애니메이션 재생
            speed += amount; // 속도 증가
            Invoke("ResetSpeed", duration); // 지정된 시간이 지나면 속도 초기화
        }
    }

    /// <summary>
    /// 속도를 기본값(8)으로 초기화하는 함수
    /// </summary>
    public void ResetSpeed()
    {
        speed = 8f;
    }
}

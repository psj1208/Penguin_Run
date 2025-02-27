using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    private float shakeDuration = 0.6f;   // 흔들림 지속 시간
    private float shakeMagnitude = 0.3f;    // 흔들림 강도

    // 무적 상태 관련 변수
    private float damageInvTime;          // 무적 지속 시간
    private float damageInvDurationTime;  // 무적 경과 시간
    [SerializeField] private bool isInvincibility; // 무적 여부
    public bool IsInvincibility => isInvincibility;

    private void Awake()
    {
        // 초기값 설정
        decreaseHPRatio = 1f; // 초당 체력 감소량
        damageInvTime = 4f; // 충돌 무적 시간
        damageInvDurationTime = 0f;
        isInvincibility = false;

        // 컴포넌트 가져오기
        player = GetComponent<PlayerController>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Update()
    {
        // 체력 자연 감소
        hp -= decreaseHPRatio * Time.deltaTime;

        // 체력이 0 이하일 시 게임 오버
        if (hp <= 0)
        {
            GameManager.Instance.GameOver();
            return;
        }

        // 무적 지속 시간 확인
        if (isInvincibility)
        {
            damageInvDurationTime += Time.deltaTime;
            if (damageInvDurationTime >= damageInvTime)
            {
                isInvincibility = false;
                damageInvDurationTime = 0f;
            }
        }
    }

    /// <summary>
    /// 체력 관리
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
    /// 체력 회복
    /// </summary>
    /// <param name="figure">회복할 체력량</param>
    private void Heal(float figure)
    {
        hp += figure;
        hp = hp >= maxHp ? maxHp : hp; // 최대 체력을 초과하지 않도록 설정
    }

    /// <summary>
    /// 피격
    /// 무적 상태가 아닐 경우만 적용
    /// </summary>
    /// <param name="figure">감소할 체력량 (음수 값)</param>
    private void Damage(float figure)
    {
        if (!isInvincibility)
        {
            animationHandler.Damage(); // 피격 애니메이션 재생
            StartCoroutine(ShakeCamera()); // 카메라 흔들림 실행
            hp += figure; // figure는 음수이므로 체력 감소
            isInvincibility = true; // 무적 활성화
            Invoke("ResetState", damageInvTime); // 지정된 시간이 지나면 상태 초기화
        }
    }

    /// <summary>
    /// 부스터 (일정 시간 동안 속도 증가 및 무적)
    /// </summary>
    /// <param name="amount">추가할 속도 값</param>
    /// <param name="duration">지속 시간 (초)</param>
    public void Booster(int amount, int duration)
    {
        if (amount > 0)
        {
            speed += amount; // 속도 증가
            isInvincibility = true; // 부스터 중에는 무적 상태
            animationHandler.Invincibility(true); // 무적 애니메이션 재생
            Invoke("ResetState", duration); // 지정된 시간이 지나면 속도 초기화
        }
    }

    // 상태 리셋
    public void ResetState()
    {
        speed = 8f;
        isInvincibility = false;
        animationHandler.Invincibility(false);
        Debug.Log("무적 해제");
    }

    // 카메라 흔들림 코루틴
    private IEnumerator ShakeCamera()
    {
        Transform camTransform = Camera.main.transform;
        Vector3 originalPos = camTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-4f, 4f) * shakeMagnitude * 2;
            float offsetY = Random.Range(-1.0f, 1.0f) * shakeMagnitude;
            camTransform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        camTransform.localPosition = originalPos;
    }
}

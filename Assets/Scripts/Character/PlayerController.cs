using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 상태 변수들
    private bool isDead;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isSliding;
    [SerializeField] private bool isInvincibility;

    // 점프 관련 변수
    [SerializeField] private int jumpForce;
    [SerializeField] private int jumpCount = 2;
    [SerializeField] private int score;

    // 체력 관련 변수 및 프로퍼티
    [SerializeField] private int hp = 10;
    public int Hp => hp;

    private int maxHp = 40;
    public int MaxHp => maxHp;

    // 이동 속도 및 사망 Y 좌표 관련 변수와 프로퍼티
    [SerializeField] private float speed = 8f;
    public float Speed => speed;

    [SerializeField] private float deathY = -10f;
    public float DeathY => deathY;

    // 속도 재설정 코루틴 참조 변수
    private Coroutine resetSpeed;

    // 컴포넌트 및 매니저 참조 변수
    private AnimationHandler animationHandler;
    private GameManager gameManager;
    private BoxCollider2D col;
    private Rigidbody2D rb;

    // 이벤트 선언: 체력 변화, 속도 변화, 점수 추가시 호출
    public event Action<PlayerController, int> OnChangeHp;
    public event Action<PlayerController, float> OnChangeSpeed;
    public event Action<PlayerController, int> OnAddScore;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        if (rb == null)
        {
            Debug.Log("Not Founded Rigidbody");
        }

        isDead = false;
        isJumping = false;

        gameManager = GameManager.Instance;
    }

    /// <summary>
    /// 체력이 0 이하이면 게임 오버 처리
    /// 스페이스바 입력 시 점프 활성화
    /// 왼쪽 Shift 입력 시 슬라이딩 활성화
    /// 일정 높이 이하로 떨어지면 게임 오버 처리
    /// </summary>
    private void Update()
    {
        if (hp <= 0)
        {
            isDead = true;
            gameManager.GameOver();
            return;
        }

        if (!isDead)
        {
            // 점프 입력 감지
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }

            // 슬라이딩 입력 감지
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSliding = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isSliding = false;
            }
        }

        // 플레이어가 사망 영역(높이 아래로 떨어짐)에 도달하면 게임 오버 처리
        if (transform.position.y < deathY)
        {
            gameManager.GameOver();
        }
    }

    /// <summary>
    /// 전진 이동, 점프, 슬라이딩, 바닥 감지 등의 물리 처리
    /// </summary>
    private void FixedUpdate()
    {
        if (isDead) return;

        // 전진: x축 속도를 현재 speed 값으로 설정
        Vector3 velocity = rb.velocity;
        velocity.x = speed;
        rb.velocity = velocity;

        // 점프 처리: 점프 중이면 jumpForce 만큼 위로 속도 적용, 남은 점프 횟수 감소
        if (isJumping)
        {
            if (jumpCount >= 0)
            {
                rb.velocity = Vector3.up * jumpForce;
                jumpCount--;
            }
        }

        // 슬라이딩 처리: 슬라이딩 중이면 회전 (90도), 그렇지 않으면 초기 회전값 (0도)
        if (isSliding)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // 바닥 감지: 아래 방향으로 레이캐스트를 쏴서 바닥("Ground" 레이어)과의 충돌 확인
        Debug.DrawRay(rb.position, Vector3.down * 2.5f, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 2.5f, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null)
        {
            isJumping = false;
            jumpCount = 2;
        }
    }

    /// <summary>
    /// 트리거 충돌 발생 시 상호작용 가능한 오브젝트와의 상호작용 처리
    /// </summary>
    /// <param name="collision">충돌한 콜라이더</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Interactable"))
        {
            InteractObject inter = collision.GetComponent<InteractObject>();
            if (inter == null)
                return;
            inter.OnInteraction(this);
        }
    }

    /// <summary>
    /// 체력 변경 (amount 양에 따라 회복 또는 데미지 적용)
    /// amount가 양수이면 Heal, 음수이면 Damage 처리 후 이벤트 발생
    /// </summary>
    /// <param name="amount">체력 변화량</param>
    public void ChangeHP(int amount)
    {
        if (amount >= 0)
        {
            Heal(amount);
        }
        else
        {
            Damage(-amount);
        }

        // 체력 변화 이벤트 호출 (양수, 음수에 관계없이 절대값 전달)
        if (amount >= 0)
        {
            OnChangeHp?.Invoke(this, amount);
        }
        else
        {
            OnChangeHp?.Invoke(this, -amount);
        }
    }

    /// <summary>
    /// 체력 회복
    /// </summary>
    /// <param name="amount">회복량</param>
    private void Heal(float amount)
    {
        // 체력 회복 로직
    }

    /// <summary>
    /// 데미지 수치가 0보다 작으면 무적 상태 활성화 및 속도 변경
    /// </summary>
    /// <param name="amount">데미지 수치</param>
    public void Damage(int amount)
    {
        if (amount < 0)
        {
            isInvincibility = true;
            ChangeSpeed(amount);
        }
    }

    /// <summary>
    /// 속도 변경
    /// 무적 상태일 경우 속도를 일시적으로 낮추고, InvincibilityEnd를 호출하여 무적 해제
    /// 속도 변경 이벤트 발생 후, 일정 시간 후 속도를 초기화하는 코루틴 시작
    /// </summary>
    /// <param name="amount">속도 변화량</param>
    public void ChangeSpeed(int amount)
    {
        if (isInvincibility)
        {
            speed = 2f;
            Invoke("InvincibilityEnd", 0.5f);
        }

        speed += amount;
        OnChangeSpeed?.Invoke(this, speed);

        // 기존에 실행 중인 속도 재설정 코루틴이 있다면 중지
        if (resetSpeed != null)
        {
            StopCoroutine(resetSpeed);
        }

        // 일정 시간 후 속도를 초기화하는 코루틴 시작
        StartCoroutine(ResetSpeed(3f));
    }

    /// <summary>
    /// 무적 상태 종료 및 속도 초기화
    /// </summary>
    public void InvincibilityEnd()
    {
        isInvincibility = false;
        speed = 8f;
    }

    /// <summary>
    /// 일정 시간 후 속도를 기본값(8f)으로 재설정하는 코루틴
    /// </summary>
    /// <param name="duration">지속 시간</param>
    /// <returns></returns>
    private IEnumerator ResetSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed = 8f;
        OnChangeSpeed.Invoke(this, speed);
    }

    /// <summary>
    /// 점수 추가 이벤트 호출
    /// </summary>
    /// <param name="amount">추가할 점수</param>
    public void AddScore(int amount)
    {
        OnAddScore?.Invoke(this, amount);
    }
}

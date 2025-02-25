using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 상태 변수들
    private bool isDead; // 플레이어가 죽었는지 여부
    [SerializeField] private bool isJumping; // 점프 상태 여부
    [SerializeField] private bool isSliding; // 슬라이딩 상태 여부

    // 점프 관련 변수
    [SerializeField] private int jumpForce; // 점프 힘
    [SerializeField] private int jumpCount = 2; // 남은 점프 가능 횟수

    [SerializeField] private float deathY = -10f; // 사망 Y축 좌표
    public float DeathY => deathY;

    // 컴포넌트 및 매니저 참조 변수
    private GameManager gameManager; // 게임 매니저 참조
    private StatHandler statHandler; // 상태 관리 핸들러
    public StatHandler Stat => statHandler;
    public AnimationHandler animationHandler; // 애니메이션 핸들러
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트 참조

    // 이벤트 선언: 체력 변화, 속도 변화, 점수 추가 시 호출
    public event Action<PlayerController, int> OnAddScore;

    private void Awake()
    {
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        statHandler = GetComponent<StatHandler>();
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
        animationHandler.Move();

        if (!isDead)
        {
            // 점프 입력 감지
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animationHandler.Jump();
                isJumping = true;
            }

            // 슬라이딩 입력 감지
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animationHandler.Slide();
                isSliding = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animationHandler.Move();
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
        if (isDead)
            return;

        Move();
        Jump();
        Sliding();
    }

    /// <summary>
    /// 플레이어 이동 처리
    /// 현재 속도를 statHandler.Speed 값으로 설정
    /// </summary>
    public void Move()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = statHandler.Speed;
        rb.velocity = velocity;
    }

    /// <summary>
    /// 플레이어 점프 처리
    /// 남은 점프 횟수가 있을 경우 점프를 수행하고 점프 횟수를 감소
    /// </summary>
    public void Jump()
    {
        if (isJumping)
        {
            if (jumpCount > 0)
            {
                Vector2 velocity = rb.velocity * 0;
                rb.velocity = velocity;
                Vector2 vel = rb.velocity + Vector2.up * jumpForce;
                rb.velocity = vel;
                --jumpCount;
                isJumping = false;
            }
        }
    }

    /// <summary>
    /// 플레이어 슬라이딩 처리
    /// 슬라이딩 중이면 90도로 회전, 그렇지 않으면 원래 상태 유지
    /// </summary>
    public void Sliding()
    {
        if (isSliding)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
            inter.OnInteraction(statHandler);
        }
    }

    /// <summary>
    /// 지면과의 충돌 감지하여 점프 횟수 초기화
    /// </summary>
    /// <param name="collision">충돌한 오브젝트 정보</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
        }
    }

    /// <summary>
    /// 점수 추가 이벤트 호출
    /// </summary>
    /// <param name="amount">추가할 점수</param>
    public void AddScore(int amount = 1)
    {
        OnAddScore?.Invoke(this, amount);
    }
}
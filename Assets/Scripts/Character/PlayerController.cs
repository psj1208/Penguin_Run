using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 상태 변수들
    private bool isDead;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isSliding;

    // 점프 관련 변수
    [SerializeField] private int jumpForce;
    [SerializeField] private int jumpCount = 2;

    [SerializeField] private float deathY = -10f;
    public float DeathY => deathY;

    // 컴포넌트 및 매니저 참조 변수
    private GameManager gameManager;
    private StatHandler statHandler;
    public StatHandler Stat => statHandler;
    public AnimationHandler animationHandler;
    private Rigidbody2D rb;

    // 이벤트 선언: 체력 변화, 속도 변화, 점수 추가시 호출
    public event Action<PlayerController, float> OnChangeSpeed;
    public event Action<PlayerController, int> OnAddScore;

    private void Awake()
    {
        isDead = false;
        jumpForce = 20;
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

    // x축 속도를 현재 speed 값으로 설정
    public void Move()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = statHandler.Speed;
        rb.velocity = velocity;
    }

    //  점프 중이면 jumpForce 만큼 위로 속도 적용, 남은 점프 횟수 감소
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

    // 슬라이딩 중이면 회전 (90도), 그렇지 않으면 초기 회전값 (0도)
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
            // 주석해제 필요
            inter.OnInteraction(statHandler);
        }
    }

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
        // 주석해제 필요
        OnAddScore?.Invoke(this, amount);
    }
}
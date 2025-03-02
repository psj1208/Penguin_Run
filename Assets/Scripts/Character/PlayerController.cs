using DataDeclaration;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool isDead; // 플레이어가 죽었는지 여부
    [SerializeField] private bool isJumping; // 점프 상태 여부
    [SerializeField] private bool isSliding; // 슬라이딩 상태 여부

    [SerializeField] private int jumpForce; // 점프 힘
    [SerializeField] private int jumpCount = 2; // 남은 점프 가능 횟수

    [SerializeField] private float deathY = -8f; // 사망 Y축 좌표
    public float DeathY => deathY;

    public AudioClip JumpClip;

    // 참조 변수
    private GameManager gameManager; // 게임 매니저 참조
    private StatHandler statHandler; // 상태 관리 핸들러
    private AnimationHandler animationHandler; // 애니메이션 핸들러 참조
    public StatHandler Stat => statHandler; // 스탯 핸들러 참조
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트 참조
    private GameObject highCollider; // highCollider 오브젝트 참조


    // 점수 추가 이벤트
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
        highCollider = transform.Find("HighCollider")?.gameObject; // 동적으로 오브젝트 찾기
        if (rb == null)
        {
            Debug.Log("Not Founded Rigidbody");
        }

        isDead = false;
        isJumping = false;
        gameManager = GameManager.Instance;

        animationHandler.Move();
    }

    private void Update()
    {
        // 지면과의 충돌 감지하여 점프 횟수 초기화
        Debug.DrawRay(rb.position, Vector3.down * 2.0f, Color.green);
        if (Physics2D.Raycast(rb.position, Vector3.down, 2.0f, LayerMask.GetMask("Ground")))
        {
            animationHandler.SetJump(false);
            jumpCount = 2;
        }

         // 플레이어가 사망 영역(높이 아래로 떨어짐)에 도달하면 게임 오버 처리
        if (transform.position.y < deathY)
        {
            gameManager.GameOver();
        }
    }

    // 전진 이동, 점프, 슬라이딩, 바닥 감지 등의 물리 처리
    private void FixedUpdate()
    {
        if (isDead) return;
        Move();
        Jump();
        Sliding();
    }

    /// <summary>
    /// Input System - Jump 액션 처리
    /// </summary>
    /// <param name="value">입력 값</param>
    public void OnJump(InputValue value)
    {
        if (value.isPressed && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    /// <summary>
    /// Input System - Slide 액션 처리
    /// </summary>
    /// <param name="value">입력 값</param>
    public void OnSlide(InputValue value)
    {
        if (value.isPressed) // Shift 키를 누르고 있을 때
        {
            isSliding = true;
            highCollider.SetActive(false);

        }
        else // Shift 키에서 손을 뗄 때
        {
            isSliding = false;

            highCollider.SetActive(true);
        }
    }

    // 이동
    public void Move()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = statHandler.Speed;
        rb.velocity = velocity;
    }

    // 점프
    public void Jump()
    {
        if (isJumping)
        {
            animationHandler.SetJump(true);
            JumpCounting();
        }
    }

    // 튜토리얼 용으로 별도로 분류
    public void JumpCounting()
    {
        if (jumpCount > 0)
        {
            AudioManager.PlayClip(JumpClip, AudioResType.sfx);
            rb.velocity = Vector2.zero;
            rb.velocity += Vector2.up * jumpForce;
            --jumpCount;
            isJumping = false;
        }
    }

    // 슬라이딩
    public void Sliding()
    {
        if (isSliding)
        {
            highCollider.SetActive(false);
            animationHandler.Slide(true);
        }
        else
        {
            highCollider.SetActive(true);
            animationHandler.Slide(false);
        }
    }


    /// <summary>
    /// 트리거 충돌 시 상호작용 가능한 오브젝트와의 상호작용 처리
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
    /// 점수 추가 이벤트 호출
    /// </summary>
    /// <param name="amount">추가할 점수</param>
    public void AddScore(int amount = 1)
    {
        OnAddScore?.Invoke(this, amount);
    }

}
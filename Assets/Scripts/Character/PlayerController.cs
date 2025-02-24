using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    private bool isDead;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isSliding;
    [SerializeField] private bool isInvincibility;

    [SerializeField] private int hp = 10;
    public int Hp => hp;
    private int maxHp = 40;
    public int MaxHp => maxHp;
    [SerializeField] private float speed = 8f;
    public float Speed => speed;
    [SerializeField] private float deathY = -10f;
    public float DeathY => deathY;
    [SerializeField] private int jumpForce;
    [SerializeField] private int jumpCount = 2;
    [SerializeField] private int score;

    private AnimationHandler animationHandler;
    private GameManager gameManager;

    private BoxCollider2D col;
    private Rigidbody2D rb;

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

    private void Update()
    {
        if (hp <= 0)
        {
            isDead = true;
            gameManager.GameOver();
            return;
        }

        if (isDead == false)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSliding = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isSliding = false;
            }
        }

        if (transform.position.y < deathY)
        {
            gameManager.GameOver();
        }
    }

    private void FixedUpdate()
    {
        if (isDead == true) return;

        // 전진
        Vector3 velocity = rb.velocity;
        velocity.x = speed;
        rb.velocity = velocity;

        // 점프
        if (isJumping == true)
        {
            if (jumpCount >= 0)
            {
                rb.velocity = Vector3.up * jumpForce;
                jumpCount--;
            }
        }

        // 슬라이딩
        if (isSliding == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (isSliding == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // 바닥 감지
        Debug.DrawRay(rb.position, Vector3.down * 2.5f, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 2.5f, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null)
        {
            isJumping = false;
            jumpCount = 2;
        }
    }

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

    public void ChangeHP(int amount)
    {
        if (amount >= 0)
        {
            Heal(amount);
        }
        else
            Damage(-amount);

        if (amount >= 0)
        {
            OnChangeHp?.Invoke(this, amount);
        }
        else
            OnChangeHp?.Invoke(this, -amount);
    }

    // 지속 시간 추가
    public void ChangeSpeed(float amount)
    {
        if (isInvincibility == true)
        {
            speed = 2f;
            Debug.Log("2");
            Invoke("InvincibilityEnd", 0.5f);
        }

        OnChangeSpeed?.Invoke(this, speed);
    }

    // 점수 추가
    public void AddScore(int amount)
    {
        OnAddScore?.Invoke(this, amount);
    }

    // 부딫힐 경우
    public void Damage(int amount)
    {
        if (amount < 0)
        {
            Debug.Log("1");
            isInvincibility = true;
            ChangeSpeed(amount);
        }
    }

    private void Heal(float amount)
    {

    }

    // 무적 해제
    public void InvincibilityEnd()
    {
        isInvincibility = false;
        Debug.Log("3");
        speed = 8f;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    private bool isDead;
    [SerializeField] private bool isJumping;
    private bool isSliding;

    [SerializeField] private int hp = 10;
    public int Hp => hp;
    private int maxHp;
    public int MaxHp => maxHp;
    private float speed = 5f;
    public float Speed => speed;
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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    void Start()
    {
        if (rb == null)
        {
            Debug.Log("Not Founded Rigidbody");
        }

        isDead = false;
        isJumping = false;

        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (hp <= 0)
        {
            isDead = true;
            gameManager.GameOver();
            return;
        }

        if (isDead == false)
        {

            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDead == true) return;

        Vector3 velocity = rb.velocity;
        velocity.x = speed;
        rb.velocity = velocity;

        if (isJumping == true)
        {
            Jump();
        }

        Debug.DrawRay(rb.position, Vector3.down, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 2.5f, LayerMask.GetMask("Ground"));

        if (rayHit.collider != null)
        {
            isJumping = false;
            jumpCount = 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Interactable"))
        {
            InteractObject inter = other.GetComponent<InteractObject>();
            if(inter == null)
                return;
            inter.OnInteraction(this);
        }
    }

    void Jump()
    {
        if (jumpCount >= 0)
        {
            rb.velocity = Vector3.up * jumpForce;
            jumpCount--;
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
        OnChangeHp?.Invoke(this, hp);
    }

    public void ChangeSpeed(float amount)
    {
        OnChangeSpeed?.Invoke(this, speed);
    }

    public void AddScore(int amount)
    {
        OnAddScore?.Invoke(this, score);
    }

    private void Damage(float amount)
    {

    }

    private void Heal(float amount)
    {

    }
}
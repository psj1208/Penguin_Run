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

    public int Hp { get; } = 10;
    public int MaxHp { get; } = 40;
    public float Speed { get; } = 5;
    [SerializeField] private int jumpForce;
    [SerializeField] private int jumpCount = 2;
    [SerializeField] private int score;

    private AnimationHandler animationHandler;
    private GameManager gameManager;

    private BoxCollider2D col;
    private Rigidbody2D rb;

    public event Action<PlayerController, int>? OnChangeHp;
    public event Action<PlayerController, float>? OnChangeSpeed;
    public event Action<PlayerController, int>? OnAddScore;

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
    }

    void Update()
    {
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
        velocity.x = Speed;
        rb.velocity = velocity;

        if (isJumping == true)
        {
            Jump();
        }

        Debug.DrawRay(rb.position, Vector3.down, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1f, LayerMask.GetMask("Ground"));

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
        OnChangeHp?.Invoke(this, Hp);
    }

    public void ChangeSpeed(float amount)
    {
        OnChangeSpeed?.Invoke(this, Speed);
    }

    public void AddScore(int amount)
    {
        OnAddScore?.Invoke(this, score);
    }

    public void Die()
    {
        if (Hp <= 0)
        {
            isDead = true;
            gameManager.GameOver();
        }
    }

    private void Damage(float amount)
    {

    }

    private void Heal(float amount)
    {

    }
}
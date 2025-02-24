using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDead;
    [SerializeField] private bool isJumping;
    private bool isSliding;

    [SerializeField] public int hp = 10;
    [SerializeField] public int maxHp = 40;
    [SerializeField] private float speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private int jumpCount = 2;

    private AnimationHandler animationHandler;
    private GameManager gameManager;

    private BoxCollider2D col;
    private Rigidbody2D rb;

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
        velocity.x = speed;
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

    }

    public void ChangeSpeed(float amount)
    {

    }

    public void Die()
    {
        if (hp <= 0)
        {
            gameManager.GameOver();
        }
    }
}
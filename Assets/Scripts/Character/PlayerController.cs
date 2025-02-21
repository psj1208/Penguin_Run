using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDead;
    private bool isSliding;
    private bool isJump;
    private int jumpCount;

    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private BoxCollider2D col;
    private Rigidbody2D rb;
}

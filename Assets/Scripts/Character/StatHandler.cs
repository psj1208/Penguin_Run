using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private float hp;
    public float Hp => hp;

    private float maxHp;
    public float MaxHp => maxHp;

    private float speed;
    public float Speed => speed;

    private float decreaseHPRatio;

    private float invincibilityTime;
    private float invincibilityDurationTime;
    private bool isInvincibility;

    private float knockbackTime;
    private float knockbackDurationTime;
    private bool isKnockback;
    public float KnockbackPower;
    public bool IsKnockback => isKnockback;

    private void Awake()
    {
        hp = 10f;
        maxHp = 40f;
        speed = 8f;
        decreaseHPRatio = 1f;
        invincibilityTime = 3f;
        invincibilityDurationTime = 0f;
        isInvincibility = false;

        knockbackTime = 0.5f;
        knockbackDurationTime = 0f;
        isKnockback = false;
        KnockbackPower = 11f;

        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        hp -= decreaseHPRatio * Time.deltaTime;

        if (hp <= 0)
        {
            // 플레이어 사망 처리
            GameManager.Instance.GameOver();
            return;
        }

        if (isInvincibility)
        {
            invincibilityDurationTime += Time.deltaTime;
            if (invincibilityDurationTime >= invincibilityTime)
            {
                isInvincibility = false;
                invincibilityDurationTime = 0f;
            }
        }

        if (isKnockback)
        {
            knockbackDurationTime += Time.deltaTime;
            if (knockbackDurationTime >= knockbackTime)
            {
                isKnockback = false;
                knockbackDurationTime = 0f;
            }
        }
    }

    public void ChangeHP(float figure)
    {
        if (figure > 0f)
        {
            Heel(figure);
        }
        else
        {
            Damage(figure);
        }
    }

    private void Heel(float figure)
    {
        hp += figure;
        hp = hp >= maxHp ? maxHp : hp;
    }

    private void Damage(float figure)
    {
        if (!isInvincibility)
        {
            isInvincibility = true;
            hp += figure;
            isKnockback = true;
        }
    }
}

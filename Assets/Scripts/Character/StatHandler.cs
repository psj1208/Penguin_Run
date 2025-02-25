using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    private PlayerController player;

    [SerializeField, Range(0f, 100f)] private float hp;
    public float Hp => hp;

    [SerializeField, Range(0f, 100f)] private float maxHp;
    public float MaxHp => maxHp;

    [SerializeField, Range(0f, 100f)] private float speed;
    public float Speed => speed;

    private float decreaseHPRatio;

    private float invincibilityTime;
    private float invincibilityDurationTime;
    [SerializeField] private bool isInvincibility;

    private void Awake()
    {
        //hp = 10f;
        //maxHp = 40f;
        //speed = 8f;
        decreaseHPRatio = 1f;
        invincibilityTime = 3f;
        invincibilityDurationTime = 0f;
        isInvincibility = false;

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
        }
    }

    public void ChangeSpeed(int amount, int duration)
    {
        if (amount > 0) 
        {
            Booster(amount, duration);
        }
    }

    public void Booster(int amount, int duration)
    {
        if(amount > 0)
        {
            isInvincibility = true;
            speed += amount;
            Invoke("ResetSpeed", duration);
        }
    }

    public void ResetSpeed()
    {
        speed = 8f;
    }
}

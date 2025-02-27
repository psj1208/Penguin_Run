using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : InteractObject
{
    [SerializeField] private int damage = 5;

    private void Awake()
    {
        sfx = Resources.Load<AudioClip>("Sounds/Damage/damage1");
    }

    public override void OnInteraction(StatHandler sHandler)
    {
        //플레이어 데미지 입는 함수
        if (!sHandler.IsInvincibility)
        {
            AudioManager.PlayClip(sfx, AudioResType.sfx);
        }
        sHandler.ChangeHP(-damage);
    }
}

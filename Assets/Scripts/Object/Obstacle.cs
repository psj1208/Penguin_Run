using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : InteractObject
{
    [SerializeField] private int damage = 5;
    GameObject player = null;

    public override void OnInteraction(PlayerController pController)
    {
        //플레이어 데미지 입는 함수
        Debug.Log($"{damage} 데미지.");
    }
}

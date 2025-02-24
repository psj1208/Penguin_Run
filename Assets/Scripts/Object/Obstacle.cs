using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : InteractObject
{
    [SerializeField] private int damage = 5;
    GameObject player = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            player = collision.gameObject;
            OnInteraction();
            Destroy(player);
        }
    }
    protected override void OnInteraction()
    {
        //플레이어 데미지 입는 함수
    }
}

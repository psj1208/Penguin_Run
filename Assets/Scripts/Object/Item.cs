using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : InteractObject
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private int hpValue = 0;
    [SerializeField] private int speedValue = 0;
    [SerializeField] private int Score = 0;
    GameObject player = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            player = collision.gameObject;
            OnInteraction();
            Destroy(gameObject);
        }
    }

    protected override void OnInteraction()
    {
        if (player == null)
            return;
        StatHandler stat = player.GetComponent<StatHandler>();
        if (stat == null)
            return;
        switch (itemType)
        {
            case ItemType.Heal:
                stat.ChangeHP(hpValue);
                break;
            case ItemType.Speed:
                stat.ChangeSpeed(speedValue);
                break;
            case ItemType.Score:
                //매니저에서 스코어 증가 함수 필요
                break;
            default:
                break;
        }
    }
}

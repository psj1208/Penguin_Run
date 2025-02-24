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
        PlayerController pController = player.GetComponent<PlayerController>();
        if (pController == null)
            return;
        switch (itemType)
        {
            case ItemType.Heal:
                pController.ChangeHP(hpValue);
                break;
            case ItemType.Speed:
                pController.ChangeSpeed(speedValue);
                break;
            case ItemType.Score:
                //매니저에서 스코어 증가 함수 필요
                break;
            default:
                break;
        }
    }
}

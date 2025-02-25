using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class Item : InteractObject
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private int hpValue = 0;
    [SerializeField] private int speedValue = 0;
    [SerializeField] private int durationValue = 0;
    [SerializeField] private int scoreValue = 0;
    ParticleSystem particle;
    bool isActive = true;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public override void OnInteraction(StatHandler statHandler)
    {
        if (statHandler == null || isActive == false)
            return;
        ParticleAndDestroy();
        switch (itemType)
        {
            case ItemType.Heal:
                UIManager.Instance.HPItemFX(this.transform.position, hpValue);
                statHandler.ChangeHP(hpValue);
                break;
            case ItemType.Speed:
                statHandler.ChangeSpeed(speedValue, durationValue);
                break;
            case ItemType.Score:
                UIManager.Instance.ScoreItemFX(this.transform.position, scoreValue);
                GameManager.Instance.Player.AddScore(scoreValue);
                break;
            default:
                break;
        }
    }
    
    void ParticleAndDestroy()
    {
        particle.Play();
        isActive = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
}

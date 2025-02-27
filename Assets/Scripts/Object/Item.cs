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
    [SerializeField] private int magneticPower;
    [SerializeField] private int magneticDuration;
    [SerializeField] private GameObject magnetic;
    ParticleSystem particle;
    bool isActive = true;

    private void Awake()
    {
        sfx = Resources.Load<AudioClip>("Sounds/Coin/coin01");
    }

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public override void OnInteraction(StatHandler statHandler)
    {
        if (statHandler == null || isActive == false)
            return;
        gameObject.layer = default;
        ParticleAndDestroy();
        switch (itemType)
        {
            case ItemType.Heal:
                UIManager.Instance.HPItemFX(this.transform.position, hpValue);
                statHandler.ChangeHP(hpValue);
                break;
            case ItemType.Speed:
                statHandler.Booster(speedValue, durationValue);
                break;
            case ItemType.Score:
                UIManager.Instance.ScoreItemFX(this.transform.position, scoreValue);
                GameManager.Instance.Player.AddScore(scoreValue);
                break;
            case ItemType.Magnetic:
                GameObject prefab = Instantiate(magnetic, statHandler.transform);
                prefab.GetComponent<Magnetic>().Init(magneticPower,magneticDuration);
                break;
            default:
                break;
        }
        AudioManager.PlayClip(sfx,AudioResType.sfx);
    }
    
    void ParticleAndDestroy()
    {
        particle.Play();
        isActive = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
}

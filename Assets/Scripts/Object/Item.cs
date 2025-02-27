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
    //아이템 타입에 따른 다른 메서드 호출.
    public override void OnInteraction(StatHandler statHandler)
    {
        if (statHandler == null || isActive == false)
            return;
        //파티클까지 자석 아이템이 끌어들이므로 방지 코드.
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
    //흭득 시 파티클 재생 후 재흭득 방지 및 스프라이트 숨기기.(파괴된 것처럼 연출)
    void ParticleAndDestroy()
    {
        particle.Play();
        isActive = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
}

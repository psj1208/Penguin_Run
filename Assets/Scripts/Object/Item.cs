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
    [SerializeField] private int durationValue = 0;
    [SerializeField] private int Score = 0;
    ParticleSystem particle;
    bool isActive = true;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public override void OnInteraction(PlayerController pController)
    {
        if (pController == null || isActive == false)
            return;
        ParticleAndDestroy();
        switch (itemType)
        {
            case ItemType.Heal:
                Debug.Log($"{hpValue} 체력 조절!");
                UIManager.Instance.HPItemFX(this.transform.position, hpValue, pController);
                pController.ChangeHP();
                break;
            case ItemType.Speed:
                Debug.Log($"{speedValue} 속도 조절!");
                pController.ChangeSpeed(speedValue);
                pController.ResetSpeed(durationValue);
                break;
            case ItemType.Score:
                //매니저에서 스코어 증가 함수 필요
                Debug.Log($"{Score} 스코어 증가!");
                UIManager.Instance.ScoreItemFX(this.transform.position, Score, pController);
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

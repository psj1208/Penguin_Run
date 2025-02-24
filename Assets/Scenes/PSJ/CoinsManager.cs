using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] Canvas mainCanvas;
    [SerializeField] RectTransform target;

    [Space]
    [Header("Available conis : (conis to pool)")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation setiings")]
    [SerializeField]
    [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField]
    [Range(0.9f, 2f)] float maxAnimDuration;
    [SerializeField] GameObject animatedCoinPrefab;

    Vector3 targetPosition;

    private int _c = 0;
    public int Coins
    {
        get { return _c; }
        set
        {
            _c = value;
        }
    }

    private void Awake()
    {
        targetPosition = target.position;

        PrepareCoins();
    }

    void PrepareCoins()
    {
        for (int i = 0; i < maxCoins; i++)
        {
            GameObject coin = Instantiate(animatedCoinPrefab);
            coin.transform.parent = transform;
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }

    void Animate(Vector3 collectedCoinPostion, int amount)
    {
        for(int i=0; i<amount; i++)
        {
            if(coinsQueue.Count > 0)
            {
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                coin.transform.position = collectedCoinPostion;

                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetPosition, duration)
                    .SetEase(Ease.InOutBack)
                    .OnComplete(() =>
                    {
                        coin.SetActive(false);
                        coinsQueue.Enqueue(coin);

                        Coins++;
                    });
            }
        }
    }

    public void AddCoins(Vector3 collectedCoinPostion,int amount)
    {
        Animate(collectedCoinPostion, amount);
    }
}

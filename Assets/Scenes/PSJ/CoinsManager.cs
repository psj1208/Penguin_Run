using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] Canvas poolingCanvas;
    [SerializeField] RectTransform coinTarget;
    [SerializeField] RectTransform heartTarget;

    [Space]
    [Header("Available conis : (conis to pool)")]
    [SerializeField] int maxCoins;
    [SerializeField] int maxHeart;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();
    Queue<GameObject> heartQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation setiings")]
    [SerializeField]
    [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField]
    [Range(0.9f, 2f)] float maxAnimDuration;
    [SerializeField] GameObject animatedCoinPrefab;
    [SerializeField] GameObject animatedHeartPrefab;

    private int _c = 0;
    public int Coins
    {
        get { return _c; }
        set
        {
            _c = value;
        }
    }

    private int _h = 0;
    public int Hearts
    {
        get { return _h; }
        set
        {
            _h = value;
        }
    }

    private void Awake()
    {
        Prepare();
    }

    void Prepare()
    {
        for (int i = 0; i < maxCoins; i++)
        {
            GameObject coin = Instantiate(animatedCoinPrefab);
            coin.transform.SetParent(poolingCanvas.transform);
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
        for (int i = 0; i < maxHeart; i++)
        {
            GameObject heart = Instantiate(animatedHeartPrefab);
            heart.transform.SetParent(poolingCanvas.transform);
            heart.SetActive(false);
            heartQueue.Enqueue(heart);
        }
    }

    void AnimateCoin(Vector3 collectedPostion, int amount)
    {
        for(int i=0; i<amount; i++)
        {
            if(coinsQueue.Count > 0)
            {
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                coin.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasInOverlay(collectedPostion);
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.GetComponent<RectTransform>().DOMove(coinTarget.position, duration)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        coin.SetActive(false);
                        coinsQueue.Enqueue(coin);

                        Coins++;
                    });
            }
        }
    }

    void AnimateHeart(Vector3 collectedPostion, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (heartQueue.Count > 0)
            {
                GameObject heart = heartQueue.Dequeue();
                heart.SetActive(true);

                heart.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasInOverlay(collectedPostion);
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                heart.GetComponent<RectTransform>().DOMove(heartTarget.position, duration)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        heart.SetActive(false);
                        heartQueue.Enqueue(heart);

                        Hearts++;
                    });
            }
        }
    }

    public void AddCoins(Vector3 collectedCoinPosition,int amount)
    {
        AnimateCoin(collectedCoinPosition, amount);
    }

    public void AddHearts(Vector3 collectedHeartPosition,int amount)
    {
        AnimateHeart(collectedHeartPosition, amount);
    }

    private Vector2 WorldToCanvasInOverlay(Vector2 world)
    {
        Vector2 screen = Camera.main.WorldToScreenPoint(world);

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(poolingCanvas.GetComponent<RectTransform>(),screen, null,out Vector2 localPos))
        {
            return localPos;
        }
        return Vector2.zero;
    }

    private Vector2 WorldToCanvas(Vector2 world)
    {
        Vector2 screen = Camera.main.WorldToScreenPoint(world);
        return screen;
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFX : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] RectTransform scoreUIObject;
    [SerializeField] RectTransform playerHPObject;

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

    private void Awake()
    {
        Prepare();
    }

    private void Prepare()
    {
        for (int i = 0; i < maxCoins; i++)
        {
            GameObject coin = Instantiate(animatedCoinPrefab);
            coin.transform.SetParent(gameObject.transform);
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
        for (int i = 0; i < maxHeart; i++)
        {
            GameObject heart = Instantiate(animatedHeartPrefab);
            heart.transform.SetParent(gameObject.transform);
            heart.SetActive(false);
            heartQueue.Enqueue(heart);
        }
    }

    public void AnimateCoin(Vector3 collectedPostion, int amount, PlayerController pControl)
    {
        for (int i = 0; i < amount; i++)
        {
            if (coinsQueue.Count > 0)
            {
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                coin.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasInOverlay(collectedPostion);
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.GetComponent<RectTransform>().DOMove(scoreUIObject.position, duration)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        coin.SetActive(false);
                        coinsQueue.Enqueue(coin);
                        pControl.AddScore();
                    });
            }
        }
    }

    public void AnimateHeart(Vector3 collectedPostion, int amount, PlayerController pControl)
    {
        for (int i = 0; i < amount; i++)
        {
            if (heartQueue.Count > 0)
            {
                GameObject heart = heartQueue.Dequeue();
                heart.SetActive(true);

                heart.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasInOverlay(collectedPostion);
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                heart.GetComponent<RectTransform>().DOMove(playerHPObject.position, duration)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        heart.SetActive(false);
                        heartQueue.Enqueue(heart);
                        pControl.ChangeHP();
                    });
            }
        }
    }

    private Vector2 WorldToCanvasInOverlay(Vector2 world)
    {
        Vector2 screen = Camera.main.WorldToScreenPoint(world);

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(gameObject.GetComponent<RectTransform>(), screen, null, out Vector2 localPos))
        {
            return localPos;
        }
        return Vector2.zero;
    }
}

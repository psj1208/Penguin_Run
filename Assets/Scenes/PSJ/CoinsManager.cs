using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] Transform target;

    [Space]
    [Header("Available conis : (conis to pool)")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation setiings")]
    [SerializeField]
    [Range(0.5f, 0.9f)] float minAnimDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

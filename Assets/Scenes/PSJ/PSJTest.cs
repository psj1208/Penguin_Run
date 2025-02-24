using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSJTest : MonoBehaviour
{
    // Start is called before the first frame update
    CoinsManager coinsManager;
    float time;
    void Start()
    {
        coinsManager = FindObjectOfType<CoinsManager>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 2f)
        {
            Debug.Log("active!");
            coinsManager.AddCoins(this.transform.position, 3);
            coinsManager.AddHearts(this.transform.position, 5);
            time = 0;
        }
    }
}

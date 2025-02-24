using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffObstacles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// 충돌시 오브젝트가 obstacle 컴포넌트가 있을때 파괴
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();

        if (obstacle)
        {
           Destroy(obstacle.gameObject);
        }
    }
}

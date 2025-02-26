using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffObstacles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();

        if (obstacle)
        {
           Destroy(obstacle.gameObject);
        }
    }
}

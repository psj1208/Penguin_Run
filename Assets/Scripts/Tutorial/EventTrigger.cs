using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private int eventId;
    bool eventEnable = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && eventEnable == true)
        {
            TutoEventManager.Instance.actionList[eventId].Invoke();
            eventEnable = false;
        }
    }
}

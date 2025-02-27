using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    //콜라이더를 씌운 오브젝트가 곧 이벤트의 트리거. 진입 시 인스펙터에서 설정한 ID의 이벤트 실행.
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

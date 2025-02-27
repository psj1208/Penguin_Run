using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObject : MonoBehaviour
{
    //플레이어 오브젝트가 도착 시에 카메라의 추적을 멈추고 게임 종료 화면을 킴.
    bool isActive = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && isActive == true)
        {
            AchieveManager.Instance.AchieveRenew(3);
            Time.timeScale = 0;
            isActive = false;
            Camera.main.GetComponent<FollowCamera>().enabled = false;
            UIManager.Instance.ChangeUIState(UIState.GameOver);
        }
    }
}

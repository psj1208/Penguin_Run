using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObject : MonoBehaviour
{
    bool isActive = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && isActive == true)
        {
            Time.timeScale = 0;
            isActive = false;
            Camera.main.GetComponent<FollowCamera>().enabled = false;
            UIManager.Instance.ChangeUIState(UIState.GameOver);
        }
    }
}

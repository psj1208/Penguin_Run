using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private PlayerController player;

    UIManager uiManager;

    //테스트용 난이도 설정
    float testlevel;


    private void Update()
    {

    }

    public void StartGame()
    {
        //uiManager.SetPlayGame(); ui매니져 작업 완료시 연결
    }

    public void IncreaseLevel()
    {
        //장애물 빈도 및 속도 증가 (아직 결정된게없음)
    }

    public void GameOver()
    {
        //uiManager.SetGameOver(); ui매니져 작업 완료시 연결
    }
}

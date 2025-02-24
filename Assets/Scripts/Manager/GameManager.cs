using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private PlayerController player;

    private UIManager uiManager;

    //test용 필요없을시 삭제 예정
    float hpTime = 180f;
    int levelSpeed = 1;
    int bestScore;
    int resultScore;
    int CurrentScore;

    //프로퍼티
    public static GameManager gameManager { get { return gameManager; } }

    private void Awake()
    {
        //싱글톤 (중복 생성시 파괴)
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        //컴포넌트 정보 변수에 지정
        player = gameObject.GetComponent<PlayerController>();
        uiManager = gameObject.GetComponent<UIManager>();
    }
    /// <summary>
    /// 게임시작 시 메뉴 호출
    /// </summary>
    void Start()
    {
        Time.timeScale = 0f;

        StartGame();
    }
    /// <summary>
    /// 게임 시작 메뉴 호출
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1f;
        uiManager.ChangeUIState(UIState.Start);//상황에 필요한 이넘 값을 매게변수에 보내서 메뉴 호출 
    }
    //아직 예정 된거 없는 메서드
    public void IncreaseLevel()
    {

    }
    /// <summary>
    /// 게임 오버 메뉴 호출
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0f;
        uiManager.ChangeUIState(UIState.GameOver);
    }
}

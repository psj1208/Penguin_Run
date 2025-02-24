using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private PlayerController player;

    private UIManager uiManager;

    //프로퍼티
    public static GameManager Instance => instance;
    public PlayerController Player => player;

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
    }

    /// <summary>
    /// 게임시작 시 메뉴 호출
    /// </summary>
    private void Start()
    {
        uiManager = UIManager.Instance;

        Time.timeScale = 0f;
    }

    /// <summary>
    /// 게임 시작 메뉴 호출
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1f;
        uiManager.ChangeUIState(UIState.Game);//상황에 필요한 이넘 값을 매게변수에 보내서 메뉴 호출 
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

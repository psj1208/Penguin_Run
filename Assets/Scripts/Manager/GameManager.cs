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

    public Transform startPos;
    public Transform endPos;

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
        StartGame();
    }

    /// <summary>
    /// 게임시작 시 메뉴 호출
    /// </summary>
    private void Start()
    {
        uiManager = UIManager.Instance;
    }

    /// <summary>
    /// 게임 시작 메뉴 호출
    /// </summary>
    private void StartGame()
    {
        Time.timeScale = 1f;
        CreatePlayer();
        if (startPos != null && endPos != null && player != null)
        {
            uiManager.MiniMapOn(startPos, endPos, player.transform);
        }
    }

    /// <summary>
    /// 게임 오버 메뉴 호출
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0f;
        uiManager.ChangeUIState(UIState.GameOver);
    }

    /// <summary>
    /// 게임 시작 시 플레이어 캐릭터 생성
    /// </summary>
    private void CreatePlayer()
    {
        GameObject newObj = Resources.Load<GameObject>("Prefabs/Player/Player");
        player = GameObject.Instantiate(newObj).GetComponent<PlayerController>();
    }
}

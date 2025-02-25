using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private PlayerController player;
    public PlayerController Player => player;

    private UIManager uiManager;

    public Transform startPos;
    public Transform endPos;

    private void Awake()
    {
        instance = this;
        CreatePlayer();
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
        StartGame();
    }

    /// <summary>
    /// 게임 시작 메뉴 호출
    /// </summary>
    private void StartGame()
    {
        Time.timeScale = 1f;
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

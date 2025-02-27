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
    private AudioManager audioManager;

    public Transform startPos;
    public Transform endPos;

    private AudioClip bgm;

    private void Awake()
    {
        instance = this;
        bgm = Resources.Load<AudioClip>("Sounds/LobbyBGM/the-console-of-my-dreams-301289");
        CreatePlayer();
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
        audioManager = AudioManager.Instance;
        audioManager.BackGroundMusic(SceneType.Stage);
        uiManager.FadeOut();
        StartCoroutine(StartGame());
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
        player.gameObject.SetActive(false);
    }

    private IEnumerator StartGame()
    {
        Time.timeScale = 1f;
        player.gameObject.transform.position = new Vector2(-8f, 4f);
        player.gameObject.SetActive(true);
        if (startPos != null && endPos != null && player != null)
        {
            uiManager.MiniMapOn(startPos, endPos, player.transform);
        }
        while (player.transform.position.x <= -5f)
        {
            yield return null;
        }
        Camera.main.AddComponent<FollowCamera>();
    }
}

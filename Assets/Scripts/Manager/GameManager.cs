using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private UIManager uiManager;
    private AudioManager audioManager;

    private PlayerController player;

    public Transform startPos;
    public Transform endPos;

    public static GameManager Instance => instance;
    public PlayerController Player => player;

    private void Awake()
    {
        instance = this;
        CreatePlayer();
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
        audioManager = AudioManager.Instance;
        audioManager.BackGroundMusic((SceneType)SceneManager.GetActiveScene().buildIndex);
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
    /// 씬 로드 시 플레이어 캐릭터 생성 후 캐릭터 오브젝트 비활성화
    /// </summary>
    private void CreatePlayer()
    {
        GameObject newObj = Resources.Load<GameObject>("Prefabs/Player/Player");
        player = GameObject.Instantiate(newObj).GetComponent<PlayerController>();
        player.gameObject.SetActive(false);
    }

    /// <summary>
    /// 코루틴 게임 시작 메서드
    /// 캐릭터 시작 위치로 이동 및 캐릭터 오브젝트 활성화
    /// <para>미니맵에 시작 위치, 마지막 위치, 캐릭터 위치 설정</para>
    /// <para>특정 위치 이동 시 카메라에 FollowCamera 스크립트 추가</para>
    /// </summary>
    private IEnumerator StartGame()
    {
        Time.timeScale = 1f;

        player.gameObject.transform.position = new Vector2(-9f, 4f);
        player.gameObject.SetActive(true);

        if (startPos != null && endPos != null && player != null)
        {
            uiManager.MiniMapOn(startPos, endPos, player.transform);
        }

        while (player.transform.position.x <= -5f)
        {
            yield return null;
        }
        Camera.main.gameObject.AddComponent<FollowCamera>();
    }
}

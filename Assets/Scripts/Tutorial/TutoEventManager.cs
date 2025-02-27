using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoEventManager : MonoBehaviour
{
    public static TutoEventManager Instance;
    TutoUiManager uiManager;
    public Dictionary<int, Action> actionList = new Dictionary<int, Action>();

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        uiManager = GetComponent<TutoUiManager>();
        GenerateDictionary();
    }

    void GenerateDictionary()
    {
        actionList.Add(1, () =>
        {
            uiManager.TextHappen("이 게임은 어떻게 진행하나요?.", txtPos.Down);
            uiManager.TextHappen("먼저 앞에 있는 장애물을 뛰어넘어봅시다.\n(점프 키 입력)", txtPos.Up, KeyCode.Space, GameManager.Instance.Player.JumpCounting);
        });

        actionList.Add(2, () =>
        {
            uiManager.TextHappen("잘 하셨습니다", txtPos.Up);
        });

        actionList.Add(3, () =>
        {
            uiManager.TextHappen("이번 장애물은 크기가 크네요", txtPos.Down);
            uiManager.TextHappen("이단 점프를 해봅시다.\n(점프 키 입력)", txtPos.Up, KeyCode.Space, GameManager.Instance.Player.JumpCounting);
        });

        actionList.Add(4, () =>
        {
            uiManager.TextHappen("한 번 더 점프하세요.\n(점프 키 입력)", txtPos.Up, KeyCode.Space, GameManager.Instance.Player.JumpCounting);
        });

        actionList.Add(5, () =>
        {
            uiManager.TextHappen("점프하면 안 될 것 같은데요.", txtPos.Down);
            uiManager.TextHappen("슬라이딩을 해서 넘어가봅시다.\n(슬라이딩 키 입력)", txtPos.Up, KeyCode.LeftShift);
        });

        actionList.Add(6, () =>
        {
            uiManager.TextHappen("이건 뭐죠?.", txtPos.Down);
            uiManager.TextHappen("이건 체력 회복 아이템입니다.", txtPos.Up);
            uiManager.TextHappen("얻으면 좌측 상단의 체력이 회복됩니다.\n체력은 계속 줄어드니 주의하세요.", txtPos.Up);
        });

        actionList.Add(7, () =>
        {
            uiManager.TextHappen("이건 뭐죠?.", txtPos.Down);
            uiManager.TextHappen("이건 점수 아이템입니다.", txtPos.Up);
            uiManager.TextHappen("얻으면 좌측 상단의 점수가 증가합니다.", txtPos.Up);
        });

        actionList.Add(8, () =>
        {
            uiManager.TextHappen("이건 뭐죠?.", txtPos.Down);
            uiManager.TextHappen("이건 속도 증가 아이템입니다.", txtPos.Up);
            uiManager.TextHappen("얻으면 일정 시간 무적이 되고 속도가 빨라집니다.", txtPos.Up);
        });

        actionList.Add(9, () =>
        {
            uiManager.TextHappen("다 통과했어요!", txtPos.Down);
            uiManager.TextHappen("잘 하셨습니다.", txtPos.Up);
            uiManager.TextHappen("이제 본 게임에서 플레이해보도록 합시다!", txtPos.Up);
            AchieveManager.Instance.AchieveRenew(2);
            Camera.main.GetComponent<FollowCamera>().enabled = false;
            StartCoroutine(FadeHelper.Fade(UIManager.Instance.Fader, 0f, 1f, 2f, () => SceneManager.LoadScene(0)));
        });
    }
}

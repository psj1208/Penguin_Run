using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    private UIState curUIState;

    private StartUI startUI;
    private GameUI gameUI;
    private GameOverUI gameOverUI;
    private MiniMap miniMap;

    private void Awake()
    {
        instance = this;

        curUIState = UIState.Start;

        startUI = GetComponentInChildren<StartUI>(true);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        miniMap = GetComponentInChildren<MiniMap>(true);
    }

    private void Start()
    {
        ChangeUIState(curUIState);
    }

    /// <summary>
    /// 매개변수 uiState UI로 변경
    /// </summary>
    /// <param name="uiState">변경하려고 하는 UI</param>
    public void ChangeUIState(UIState uiState)
    {
        curUIState = uiState;
        startUI.ActiveUI(uiState);
        gameUI.ActiveUI(uiState);
        gameOverUI.ActiveUI(uiState);
    }

    /// <summary>
    /// GameOver 시 GameOverUI에서 결과 점수를 가져오기 위해 사용하는 메서드
    /// </summary>
    /// <returns>GameOver 시 결과 점수</returns>
    public int GetResultScore()
    {
        return gameUI.CurScore;
    }

    /// <summary>
    /// 점수 아이템 획득 시 호출하는 획득 이펙트 메서드
    /// </summary>
    /// <param name="collectedPostion">아이템 position</param>
    /// <param name="amount">획득량</param>
    /// <param name="pControl">플레이어</param>
    public void ScoreItemFX(Vector3 collectedPostion, int amount, PlayerController pControl)
    {
        gameUI.UIFX.AnimateCoin(collectedPostion, amount, pControl);
    }

    /// <summary>
    /// 회복 아이템 획득 시 호출하는 획득 이펙트 메서드
    /// </summary>
    /// <param name="collectedPostion">아이템 position</param>
    /// <param name="amount">회복량</param>
    /// <param name="pControl">플레이어</param>
    public void HPItemFX(Vector3 collectedPostion, int amount, PlayerController pControl)
    {
        gameUI.UIFX.AnimateHeart(collectedPostion, amount, pControl);
    }

    public void MiniMapOn(Transform st, Transform end, Transform player)
    { 
        if(miniMap != null)
            miniMap.Init(st, end, player);
    }
    /// <summary>
    /// 미니맵 좌표를 설정하는 함수. 매개변수는 비율로.(0~1)
    /// </summary>
    /// <param name="ratio"></param>
}

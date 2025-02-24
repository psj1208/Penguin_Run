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

    private void Awake()
    {
        instance = this;

        curUIState = UIState.Start;

        startUI = GetComponentInChildren<StartUI>();
        gameUI = GetComponentInChildren<GameUI>();
        gameOverUI = GetComponentInChildren<GameOverUI>();
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
}

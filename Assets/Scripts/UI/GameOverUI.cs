using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private UIManager uiManager;

    private const UIState state = UIState.GameOver;

    private int bestScore;
    private int resultScore;

    [SerializeField] private TextMeshProUGUI bestScoreTxt;
    [SerializeField] private TextMeshProUGUI resultScoreTxt;
    [SerializeField] private Button RestartBtn;
    [SerializeField] private Button ExitBtn;

    private void Awake()
    {
        RestartBtn.onClick.AddListener(OnclickRestartButton);
        ExitBtn.onClick.AddListener(OnclickExitButton);
    }

    private void OnEnable()
    {
        resultScore = uiManager.GetResultScore();
        bestScore = PlayerPrefs.GetInt("BestScore", resultScore);
        if (resultScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", resultScore);
            bestScore = resultScore;
        }
        bestScoreTxt.text = $"Best Score: {bestScore.ToString()}";
        resultScoreTxt.text = $"Result Score: {resultScore.ToString()}";
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
    }

    /// <summary>
    /// 게임오브젝트 활성화/비활성화
    /// </summary>
    public void ActiveUI(UIState uiState)
    {
        gameObject.SetActive(state == uiState);
    }

    private void OnclickRestartButton()
    {
        Debug.Log("재시작 버튼 누름");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnclickExitButton()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}

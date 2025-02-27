using DataDeclaration;
using DG.Tweening;
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
    private AudioClip gameOverSfx;

    [SerializeField] private RectTransform panel;
    [SerializeField] private TextMeshProUGUI bestScoreTxt;
    [SerializeField] private TextMeshProUGUI resultScoreTxt;
    [SerializeField] private Button RestartBtn;
    [SerializeField] private Button ExitBtn;

    private void Awake()
    {
        gameOverSfx = Resources.Load<AudioClip>("Sounds/Clear/you-win-sequence-1-183948");

        RestartBtn.onClick.AddListener(OnclickRestartButton);
        ExitBtn.onClick.AddListener(OnclickExitButton);
    }

    private void OnEnable()
    {
        if (uiManager != null)
        {
            panel.localScale = Vector3.zero;
            panel.DOScale(1, 1).SetEase(Ease.OutBounce).SetUpdate(true);
            AudioManager.Instance.BackGroundMusic(SceneType.None);
            AudioManager.PlayClip(gameOverSfx,AudioResType.sfx);
            resultScore = uiManager.GetResultScore();
            bestScore = PlayerPrefs.GetInt("BestScore", 0);
            if (resultScore > bestScore)
            {
                PlayerPrefs.SetInt("BestScore", resultScore);
                bestScore = resultScore;
            }
            bestScoreTxt.text = $"Best Score: {bestScore.ToString()}";
            resultScoreTxt.text = $"Result Score: {resultScore.ToString()}";
        }
    }

    private void Start()
    {
        uiManager = UIManager.Instance;

        panel.localScale = Vector3.zero;
        panel.DOScale(1, 1).SetEase(Ease.OutBounce).SetUpdate(true);
        AudioManager.Instance.BackGroundMusic(SceneType.None);
        AudioManager.PlayClip(gameOverSfx,AudioResType.sfx);
        resultScore = uiManager.GetResultScore();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (resultScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", resultScore);
            bestScore = resultScore;
        }
        bestScoreTxt.text = $"Best Score: {bestScore.ToString()}";
        resultScoreTxt.text = $"Result Score: {resultScore.ToString()}";
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
        Time.timeScale = 1.0f;
        StartCoroutine(FadeHelper.Fade(UIManager.Instance.Fader, 0f, 1f, 0.5f));
        SceneManager.LoadScene(0);
    }
}

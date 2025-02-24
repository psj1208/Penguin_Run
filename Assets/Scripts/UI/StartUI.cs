using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    private GameManager gameManager;
    private UIManager uiManager;

    private const UIState state = UIState.Start;

    [SerializeField] private Button StartBtn;
    [SerializeField] private Button ExitBtn;

    private void Awake()
    {
        StartBtn.onClick.AddListener(OnClickStartButton);
        ExitBtn.onClick.AddListener(OnClickExitButton);
    }

    private void Start()
    {
        //gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
    }

    /// <summary>
    /// 게임오브젝트 활성화/비활성화
    /// </summary>
    public void ActiveUI(UIState uiState)
    {
        gameObject.SetActive(state == uiState);
    }

    private void OnClickStartButton()
    {
        uiManager.ChangeUIState(UIState.Game);
        gameManager.StartGame();
    }

    private void OnClickExitButton()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}

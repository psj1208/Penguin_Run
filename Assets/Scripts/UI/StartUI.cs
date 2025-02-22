using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
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
        uiManager = UIManager.Instance;
    }

    public void ActiveUI(UIState uiState)
    {
        gameObject.SetActive(state == uiState);
    }

    private void OnClickStartButton()
    {
        uiManager.ChangeUIState(UIState.Game);
    }

    private void OnClickExitButton()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}

using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private int curScore;

    private const UIState state = UIState.Game;

    [SerializeField] private TextMeshProUGUI curScoreTxt;
    [SerializeField] private Slider playerHPSlider;

    private void Awake()
    {
        curScore = 0;
        curScoreTxt.text = curScore.ToString();
        playerHPSlider.value = 1f;
    }

    private void Update()
    {
        playerHPSlider.value -= 0.01f * Time.deltaTime;
    }

    public void ActiveUI(UIState uiState)
    {
        gameObject.SetActive(state == uiState);
    }

    public void UpdateCurrentScore(int figure)
    {
        ++curScore;
        curScoreTxt.text = curScore.ToString();
    }

    public void ChangePlayerHP(int figure)
    {
        playerHPSlider.value += figure;
    }
}

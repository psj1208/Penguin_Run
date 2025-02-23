using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private GameManager gameManager;

    private int curScore;
    private int playerHP;

    private const UIState state = UIState.Game;

    [SerializeField] private TextMeshProUGUI curScoreTxt;
    [SerializeField] private Slider playerHPSlider;

    private void Awake()
    {
        curScore = 0;
        curScoreTxt.text = curScore.ToString();
        playerHPSlider.value = 1f;
    }

    private void Start()
    {
        //gameManager = GameManager.Instance;
        //playerHP = gameManager.Player.HP;
        //playerHPSlider.maxValue = playerHP;
        //playerHPSlider.value = playerHP;
    }

    private void Update()
    {
        playerHPSlider.value -= 0.01f * Time.deltaTime;
    }

    /// <summary>
    /// 게임오브젝트 활성화/비활성화
    /// </summary>
    public void ActiveUI(UIState uiState)
    {
        gameObject.SetActive(state == uiState);
    }

    /// <summary>
    /// 현재 점수를 변경 수치만큼 더함
    /// </summary>
    /// <param name="figure">변경 수치</param>
    public void UpdateCurrentScore(int figure)
    {
        curScore += figure;
        curScoreTxt.text = curScore.ToString();
    }

    /// <summary>
    /// 체력게이지를 변경 수치만큼 더함
    /// </summary>
    /// <param name="figure">변경 수치</param>
    public void ChangePlayerHP(int figure)
    {
        playerHPSlider.value += figure;
    }
}

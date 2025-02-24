using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private PlayerController player;

    private int curScore;
    public int CurScore => curScore;
    private int playerHP;

    private const UIState state = UIState.Game;

    [SerializeField] private TextMeshProUGUI curScoreTxt;
    [SerializeField] private Slider playerHPSlider;

    private void Awake()
    {
        curScore = 0;
        curScoreTxt.text = curScore.ToString();
    }

    private void Start()
    {
        player = GameManager.gameManager.Player;
        playerHPSlider.maxValue = player.maxHp;
        playerHPSlider.value = player.hp;

        player.OnChangeHp += ChangePlayerHP;
        //player.OnChangeScore += UpdateCurrentScore;   // 추후 주석 해제
    }

    private void Update()
    {
        playerHPSlider.value -= 0.01f * Time.deltaTime;
    }

    private void OnDisable()
    {
        player.OnChangeHp -= ChangePlayerHP;
        //player.OnChangeScore -= UpdateCurrentScore;   // 추후 주석 해제
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
    private void UpdateCurrentScore(PlayerController player, int figure)
    {
        curScore += figure;
        curScoreTxt.text = curScore.ToString();
    }

    /// <summary>
    /// 체력게이지를 변경 수치만큼 더함
    /// </summary>
    /// <param name="figure">변경 수치</param>
    private void ChangePlayerHP(PlayerController player, int figure)
    {
        playerHPSlider.value += figure;
    }
}

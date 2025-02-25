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

    private const UIState state = UIState.Game;

    [SerializeField] private UIFX uiFX;
    public UIFX UIFX => uiFX;

    [SerializeField] private TextMeshProUGUI curScoreTxt;
    [SerializeField] private Slider playerHPSlider;

    private void Awake()
    {
        curScore = 0;
        curScoreTxt.text = curScore.ToString();
    }

    private void OnEnable()
    {
        if (player != null)
        {
            player.OnAddScore += UpdateCurrentScore;
        }
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        playerHPSlider.maxValue = player.Stat.MaxHp;
        playerHPSlider.value = player.Stat.Hp;

        player.OnAddScore += UpdateCurrentScore;
    }

    private void Update()
    {
        playerHPSlider.value = player.Stat.Hp;
    }
    
    private void OnDisable()
    {
        if (player != null)
        {
            player.OnAddScore -= UpdateCurrentScore;
        }
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
}

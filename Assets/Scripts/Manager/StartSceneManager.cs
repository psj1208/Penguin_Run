using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class StartSceneManager : MonoBehaviour
{
    //현재 애니메이션이 진행 중인지 아닌지
    private bool isAnim;
    private AudioClip btnClickSFX;

    private StartMenuUI startMenuUI;
    private StageSelectUI stageSelectUI;
    private SettingUI settingUI;
    private CanvasGroup fader;

    public CanvasGroup Fader => fader;

    public AudioClip BtnClickSFX => btnClickSFX;

    private void Awake()
    {
        Time.timeScale = 1.0f;

        isAnim = false;
        btnClickSFX = Resources.Load<AudioClip>("Sounds/Coin/coin01");

        startMenuUI = GetComponentInChildren<StartMenuUI>(true);
        stageSelectUI = GetComponentInChildren<StageSelectUI>(true);
        settingUI = GetComponentInChildren<SettingUI>(true);
        fader = GetComponentInChildren<CanvasGroup>(true);
    }

    private void Start()
    {
        AudioManager.Instance.BackGroundMusic(SceneType.Start);
    }

    /// <summary>
    /// 스테이지 선택 UI On/Off 메서드
    /// </summary>
    /// <param name="activation">활성화 여부</param>
    public void ToggleStageSelectUI(bool activation)
    {
        if (activation == true && isAnim == false)
        {
            isAnim = true;
            stageSelectUI.gameObject.SetActive(true);
            stageSelectUI.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0f, 1f)
                .SetUpdate(true)
                .OnComplete(() => isAnim = false);
        }
        else if (activation == false && isAnim == false)
        {
            isAnim = true;
            stageSelectUI.gameObject.GetComponent<RectTransform>().DOAnchorPosY(1200f, 1f, false)
                .SetUpdate(true)
                .OnComplete(() => { stageSelectUI.gameObject.SetActive(false); isAnim = false; });
        }
    }

    /// <summary>
    /// 환경설정 UI On/Off 메서드
    /// </summary>
    /// <param name="activation">활성화 여부</param>
    public void ToggleSettingUI(bool activation)
    {
        if (activation == true && isAnim == false)
        {
            AchieveManager.Instance.AchieveRenew(1);
            isAnim = true;
            settingUI.gameObject.SetActive(true);
            settingUI.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0f, 1f)
                .SetUpdate(true)
                .OnComplete(() => isAnim = false);
        }
        else if (activation == false && isAnim == false)
        {
            isAnim = true;
            settingUI.gameObject.GetComponent<RectTransform>().DOAnchorPosY(1200f, 1f, false)
                .SetUpdate(true)
                .OnComplete(() => { settingUI.gameObject.SetActive(false); isAnim = false; });
        }
    }
}

using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class StartSceneManager : MonoBehaviour
{
    #region Variable
    private AudioClip btnSFX;
    bool isAnim = false;

    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private CanvasGroup fader;

    [Header("세팅 관련 UI")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Button settingExitButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private SettingBaseUI sound;
    [SerializeField] private Button controlButton;
    [SerializeField] private SettingBaseUI control;
    [SerializeField] private StageSelect stageSelect;
    #endregion

    #region Property
    public AudioClip BtnSFX => btnSFX;
    public CanvasGroup Fader => fader;
    #endregion

    #region Unity Method
    private void Awake()
    {
        stageSelect = GetComponentInChildren<StageSelect>(true);
        Time.timeScale = 1.0f;
        btnSFX = Resources.Load<AudioClip>("Sounds/Coin/coin01");

        startBtn.onClick.AddListener(OnClickStartButton);
        settingBtn.onClick.AddListener(OnClickSettingButton);
        exitBtn.onClick.AddListener(OnClickExitButton);

        settingExitButton.onClick.AddListener(OnClickSettingExitButton);
        soundButton.onClick.AddListener(OnClickSoundSettingButton);
        controlButton.onClick.AddListener(OnClickControlSettingButton);
    }

    private void Start()
    {
        settingPanel.SetActive(false);
        stageSelect.gameObject.SetActive(false);
    }
    #endregion

    #region Public Method
    public void SelectState(bool state)
    {
        GameObject stage = stageSelect.gameObject;
        if (state == true && isAnim == false)
        {
            isAnim = true;
            stage.SetActive(true);
            stage.GetComponent<RectTransform>().DOAnchorPosY(0, 1)
                .SetUpdate(true)
                .OnComplete(() => isAnim = false);
        }
        else if (state == false && isAnim == false)
        {
            isAnim = true;
            stage.GetComponent<RectTransform>().DOAnchorPosY(1200, 1f, false)
                .SetUpdate(true)
                .OnComplete(() => { stage.SetActive(false); isAnim = false; });
        }
    }
    #endregion

    #region Private Method
    private void OnClickStartButton()
    {
        AudioManager.PlayClip(btnSFX);
        SelectState(true);
    }

    private void OnClickSettingButton()
    {
        AudioManager.PlayClip(btnSFX);
        SettingState(true);
    }

    private void OnClickExitButton()
    {
        AudioManager.PlayClip(btnSFX);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private void OnClickSettingExitButton()
    {
        AudioManager.PlayClip(btnSFX);
        SettingState(false);
    }

    private void OnClickSoundSettingButton()
    {
        AudioManager.PlayClip(btnSFX);
        ChangeSettingUIState(SettingUIState.Sound);
    }

    private void OnClickControlSettingButton()
    {
        AudioManager.PlayClip(btnSFX);
        ChangeSettingUIState(SettingUIState.Control);
    }

    private void SettingState(bool state)
    {
        if (state == true && isAnim == false)
        {
            AchieveManager.Instance.AchieveRenew(1);
            isAnim = true;
            settingPanel.SetActive(true);
            settingPanel.GetComponent<RectTransform>().DOAnchorPosY(0, 1)
                .SetUpdate(true)
                .OnComplete(() => isAnim = false);
        }
        else if (state == false && isAnim == false)
        {
            isAnim = true;
            settingPanel.GetComponent<RectTransform>().DOAnchorPosY(1200, 1f, false)
                .SetUpdate(true)
                .OnComplete(() => { settingPanel.SetActive(false); isAnim = false; });
        }
    }

    private void ChangeSettingUIState(SettingUIState uistate)
    {
        sound.SetActive(uistate);
        control.SetActive(uistate);
    }
    #endregion
}

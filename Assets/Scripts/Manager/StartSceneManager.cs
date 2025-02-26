using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class StartSceneManager : MonoBehaviour
{
    private float elapsedTime;
    private float fadeTime;
    private AudioClip btnSFX;

    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Image fadeInImage;

    [Header("세팅 관련 UI")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Button settingExitButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private SettingBaseUI sound;
    [SerializeField] private Button controlButton;
    [SerializeField] private SettingBaseUI control;
    [SerializeField] private StageSelect stageSelect;
    bool isAnim = false;

    private void Awake()
    {
        stageSelect = GetComponentInChildren<StageSelect>(true);
        elapsedTime = 0f;
        fadeTime = 1f;
        btnSFX = Resources.Load<AudioClip>("Sounds/Coin/coin01");

        startBtn.onClick.AddListener(() => SelectState(true));
        startBtn.onClick.AddListener(() => { AudioManager.PlayClip(btnSFX,AudioResType.sfx); });
        settingBtn.onClick.AddListener(() => { SettingState(true); });
        exitBtn.onClick.AddListener(() => {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
        settingExitButton.onClick.AddListener(() => { SettingState(false); });
        soundButton.onClick.AddListener(() => { ChangeSettingUIState(SettingUIState.Sound); });
        controlButton.onClick.AddListener(() => { ChangeSettingUIState(SettingUIState.Control); });
        fadeInImage.color = Color.clear;
    }

    private void Start()
    {
        settingPanel.SetActive(false);
        stageSelect.gameObject.SetActive(false);
    }
    private void OnClickStartButton()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeTime;

            fadeInImage.color = Color.Lerp(Color.clear, Color.black, elapsedTime / fadeTime);

            yield return null;
        }
        fadeInImage.color = Color.black;
        SceneManager.LoadScene(1);
    }

    public void SettingState(bool state)
    {
        if (state == true && isAnim == false)
        {
            isAnim = true;
            settingPanel.SetActive(true);
            settingPanel.GetComponent<RectTransform>().DOAnchorPosY(0, 1)
                .SetUpdate(true)
                .OnComplete(() => isAnim = false);
        }
        else if(state == false && isAnim == false)
        {
            isAnim = true;
            settingPanel.GetComponent<RectTransform>().DOAnchorPosY(1200, 1f, false)
                .SetUpdate(true)
                .OnComplete(() => { settingPanel.SetActive(false); isAnim = false; });
        }
    }

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
    public void ChangeSettingUIState(SettingUIState uistate)
    {
        sound.SetActive(uistate);
        control.SetActive(uistate);
    }
}

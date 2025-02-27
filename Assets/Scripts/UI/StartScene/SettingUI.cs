using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    private StartSceneManager manager;

    [SerializeField] private Button settingExitButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button controlButton;

    [SerializeField] private SettingBaseUI sound;
    [SerializeField] private SettingBaseUI control;

    private void Awake()
    {
        manager = GetComponentInParent<StartSceneManager>();

        soundButton.onClick.AddListener(OnClickSoundSettingButton);
        controlButton.onClick.AddListener(OnClickControlSettingButton);
        settingExitButton.onClick.AddListener(OnClickSettingExitButton);
    }

    private void OnClickSoundSettingButton()
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
        ChangeSettingUIState(SettingUIState.Sound);
    }

    private void OnClickControlSettingButton()
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
        ChangeSettingUIState(SettingUIState.Control);
    }

    private void OnClickSettingExitButton()
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
        manager.ToggleSettingUI(false);
    }

    /// <summary>
    /// 설정 화면 변경을 위한 메서드
    /// </summary>
    /// <param name="uistate">enum 타입의 설정 종류</param>
    private void ChangeSettingUIState(SettingUIState uistate)
    {
        sound.SetActive(uistate);
        control.SetActive(uistate);
    }
}

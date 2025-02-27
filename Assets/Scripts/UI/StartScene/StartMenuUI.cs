using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    private StartSceneManager manager;

    [SerializeField] private Button startBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button exitBtn;

    private void Awake()
    {
        manager = GetComponentInParent<StartSceneManager>();

        startBtn.onClick.AddListener(OnClickStartButton);
        settingBtn.onClick.AddListener(OnClickSettingButton);
        exitBtn.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickStartButton()
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
        manager.ToggleStageSelectUI(true);
    }

    private void OnClickSettingButton()
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
        manager.ToggleSettingUI(true);
    }

    private void OnClickExitButton()
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

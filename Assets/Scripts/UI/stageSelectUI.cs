using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    private StartSceneManager manager;

    [SerializeField] private Button Tuto;
    [SerializeField] private Button stage1;
    [SerializeField] private Button stage2;
    [SerializeField] private Button ExitButton;

    private void Awake()
    {
        manager = GetComponentInParent<StartSceneManager>();

        Tuto.onClick.AddListener(() => OnClickStageButton(1));
        stage1.onClick.AddListener(() => OnClickStageButton(2));
        stage2.onClick.AddListener(() => OnClickStageButton(3));
        ExitButton.onClick.AddListener(OnClickEixtButton);
    }

    private void OnClickStageButton(int level)
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
        StartCoroutine(FadeHelper.Fade(manager.Fader, 0f, 1f, 1f, () => SceneManager.LoadScene(level)));
    }

    private void OnClickEixtButton()
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
        manager.ToggleStageSelectUI(false);
    }
}

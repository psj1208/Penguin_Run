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

        Tuto.onClick.AddListener(() => OnClickStageButton((int)SceneType.Tutorial));
        stage1.onClick.AddListener(() => OnClickStageButton((int)SceneType.Stage1));
        stage2.onClick.AddListener(() => OnClickStageButton((int)SceneType.Stage2));
        ExitButton.onClick.AddListener(OnClickEixtButton);
    }

    private void OnClickStageButton(int sceneIndex)
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
        StartCoroutine(FadeHelper.Fade(manager.Fader, 0f, 1f, 1f, () => SceneManager.LoadScene(sceneIndex)));
    }

    private void OnClickEixtButton()
    {
        AudioManager.PlayClip(manager.BtnClickSFX, AudioResType.sfx);
        manager.ToggleStageSelectUI(false);
    }
}

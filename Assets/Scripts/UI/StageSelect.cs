using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    private StartSceneManager sManager;

    [SerializeField] private Button Tuto;
    [SerializeField] private Button stage1;
    [SerializeField] private Button stage2;
    [SerializeField] private Button ExitButton;

    private void Awake()
    {
        Tuto.onClick.AddListener(() => OnClickStageButton(1));
        stage1.onClick.AddListener(() => OnClickStageButton(2));
        stage2.onClick.AddListener(() => OnClickStageButton(3));

        sManager = GetComponentInParent<StartSceneManager>();
        ExitButton.onClick.AddListener(OnClickEixtButton);
    }

    private void OnClickStageButton(int level)
    {
        AudioManager.PlayClip(sManager.BtnSFX, AudioResType.sfx);
        StartCoroutine(FadeHelper.Fade(sManager.Fader, 0f, 1f, 1f, () => SceneManager.LoadScene(level)));
    }

    private void OnClickEixtButton()
    {
        AudioManager.PlayClip(sManager.BtnSFX, AudioResType.sfx);
        sManager.SelectState(false);
    }
}

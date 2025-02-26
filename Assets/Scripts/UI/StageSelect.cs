using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    StartSceneManager sManager;

    [SerializeField] Button Tuto;
    [SerializeField] Button stage1;
    [SerializeField] Button stage2;
    [SerializeField] Button ExitButton;

    private void Awake()
    {
        Tuto.onClick.AddListener(() => OnClickStageButton(1));
        stage1.onClick.AddListener(() => OnClickStageButton(2));
        stage2.onClick.AddListener(() => OnClickStageButton(3));

        sManager = GetComponentInParent<StartSceneManager>();
        ExitButton.onClick.AddListener(() => sManager.SelectState(false));
    }

    private void OnClickStageButton(int level)
    {
        AudioManager.PlayClip(sManager.BtnSFX);
        StartCoroutine(FadeHelper.Fade(sManager.Fader, 0f, 1f, 1f, () => SceneManager.LoadScene(level)));
    }
}

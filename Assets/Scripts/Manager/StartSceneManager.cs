using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    private float fadeTime;
    private AudioClip btnSFX;

    [SerializeField] private Button startBtn;
    [SerializeField] private CanvasGroup fader;

    private void Awake()
    {
        fadeTime = 1f;
        btnSFX = Resources.Load<AudioClip>("Sounds/Coin/coin01");

        startBtn.onClick.AddListener(OnClickStartButton);
        startBtn.onClick.AddListener(() => { AudioManager.PlayClip(btnSFX); });
        fader.alpha = 0f;
    }

    private void OnClickStartButton()
    {
        StartCoroutine(FadeHelper.Fade(fader, 0f, 1f, fadeTime, () => SceneManager.LoadScene(1)));
    }
}

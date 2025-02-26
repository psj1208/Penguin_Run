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
        Time.timeScale = 1.0f;
        fadeTime = 1f;
        btnSFX = Resources.Load<AudioClip>("Sounds/Coin/coin01");

        startBtn.onClick.AddListener(OnClickStartButton);
        fader.alpha = 0f;
    }

    private void OnClickStartButton()
    {
        Debug.Log("버튼 누름");
        AudioManager.PlayClip(btnSFX);
        StartCoroutine(FadeHelper.Fade(fader, 0f, 1f, fadeTime, () => SceneManager.LoadScene(1)));
    }
}

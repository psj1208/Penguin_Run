using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    private float elapsedTime;
    private float fadeTime;
    private AudioClip btnSFX;

    [SerializeField] private Button startBtn;
    [SerializeField] private Image fadeInImage;

    private void Awake()
    {
        elapsedTime = 0f;
        fadeTime = 1f;
        btnSFX = Resources.Load<AudioClip>("Sounds/Coin/coin01");

        startBtn.onClick.AddListener(OnClickStartButton);
        startBtn.onClick.AddListener(() => { AudioManager.PlayClip(btnSFX,AudioResType.sfx); });
        fadeInImage.color = Color.clear;
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
}

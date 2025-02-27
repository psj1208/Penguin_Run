using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider backGroundSlider;
    [SerializeField] private Slider sfxSlider;

    /// <summary>
    /// 슬라이드 바 의 value 기준으로 음량조절
    /// </summary>
    public void MasterVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20);
    }

    public void BackGroundVolume()
    {
        audioMixer.SetFloat("BackGround", Mathf.Log10(backGroundSlider.value) * 20);
    }

    public void SfxVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
    }
}

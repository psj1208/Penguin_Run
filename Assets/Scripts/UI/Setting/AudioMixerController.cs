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


    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(MasterVolume);
        backGroundSlider.onValueChanged.AddListener(BackGroundVolume);
        sfxSlider.onValueChanged.AddListener(SfxVolume);
    }

    public void MasterVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void BackGroundVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SfxVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
}

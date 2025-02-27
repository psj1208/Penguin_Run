using DataDeclaration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundUI : SettingBaseUI
{
    private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider backGroundSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        audioMixer = Resources.Load<AudioMixer>("Sounds/AudioGroup/Volume");
    }

    /// <summary>
    /// 슬라이드 바 의 value 기준으로 음량조절
    /// </summary>
    private void MasterVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20);
    }

    private void BackGroundVolume()
    {
        audioMixer.SetFloat("BackGround", Mathf.Log10(backGroundSlider.value) * 20);
    }

    private void SfxVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
    }

    protected override SettingUIState GetUIState()
    {
        return SettingUIState.Sound;
    }

    public override void Init(StartSceneManager start)
    {
        base.Init(start);
    }
}

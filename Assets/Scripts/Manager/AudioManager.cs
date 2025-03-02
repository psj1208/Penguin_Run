using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataDeclaration;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    private Dictionary<SceneType, AudioClip> bgmAudioClipDict;

    private float musicVolume;
    private float soundEffectPitchVariance;
    private float soundEffectVolume;

    private AudioSource audioSource;

    public AudioMixerGroup master;
    public AudioMixerGroup backGround;
    public AudioMixerGroup sfx;

    private SoundSource prefabSoundSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        bgmAudioClipDict = new Dictionary<SceneType, AudioClip>();
        bgmAudioClipDict.Add(SceneType.None, null);
        bgmAudioClipDict.Add(SceneType.Start, Resources.Load<AudioClip>("Sounds/GameSceneBGM/game-music-player-console-8bit-background-intro-theme-297305"));
        bgmAudioClipDict.Add(SceneType.Tutorial, Resources.Load<AudioClip>("Sounds/LobbyBGM/game-8-bit-on-278083"));
        bgmAudioClipDict.Add(SceneType.Stage1, Resources.Load<AudioClip>("Sounds/LobbyBGM/the-console-of-my-dreams-301289"));
        bgmAudioClipDict.Add(SceneType.Stage2, Resources.Load<AudioClip>("Sounds/GameSceneBGM/i-love-my-8-bit-game-console-301272"));

        musicVolume = 0.145f;
        soundEffectPitchVariance = 0.156f;
        soundEffectVolume = 0.156f;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        audioSource.loop = true;

        prefabSoundSource = Resources.Load<SoundSource>("Prefabs/SoundSource");
    }

    private void Start()
    {
        BackGroundMusic(SceneType.Start);
    }

    /// <summary>
    /// 현재 씬에 따른 배경 음악 재생
    /// </summary>
    /// <param name="type">현재 씬 타입</param>
    public void BackGroundMusic(SceneType type)
    {
        audioSource.clip = bgmAudioClipDict[type];//클립 등록
        audioSource.outputAudioMixerGroup = backGround;//오디오 소스 output 등록
        audioSource.Stop();
        audioSource.Play();
    }

    /// <summary>
    /// 오브젝트 사운드 클립 재생 메서드
    /// </summary>
    /// <param name="clip">사운드 클립</param>
    /// <param name="type">사운드 믹스 그룹타입</param>
    public static void PlayClip(AudioClip clip,AudioResType type)
    {
        SoundSource prefab = Instantiate(instance.prefabSoundSource);   //프리펩을 복제
        SoundSource soundSource = prefab.GetComponent<SoundSource>();   //복제된 프리펩의 사운드 소스 컴포넌트 설정을 가져온다
        AudioSource audioSource = soundSource.GetComponent<AudioSource>();  //오디오 소스 컴포넌트도 가져온다
        
        if(type == AudioResType.sfx)
        {
            //가져온 오디오소스 컴포넌트에서 output 쪽에 그룹을 오디오 매니져 sfx 변수에 등록된 사운드믹스 그룹을 넣는다
            audioSource.outputAudioMixerGroup = instance.sfx;

            //가져온 사운드 소스 컴포넌트 클립 볼륨값을 오디오 매니져의 볼륨값으로 재설정뒤 Play메서드에게 보내서 재생
            soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
        }
    }
}

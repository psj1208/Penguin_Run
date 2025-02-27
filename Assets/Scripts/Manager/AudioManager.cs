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

    [SerializeField][Range(0f, 1f)] private float musicVolume = 0.145f;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance = 0.156f;
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume = 0.156f;

    private AudioSource audioSource;

    public AudioMixer audioMixer;
    public AudioMixerGroup master;
    public AudioMixerGroup backGround;
    public AudioMixerGroup sfx_;

    private SoundSource prefabSoundSource;

    private void Awake()
    {
        //싱글톤
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

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        audioSource.loop = true;

        prefabSoundSource = Resources.Load<SoundSource>("Prefabs/SoundSource");
    }
    /// <summary>
    /// 시작시 배경음악 재생
    /// </summary>
    private void Start()
    {
        BackGroundMusic(SceneType.Start);
    }
    /// <summary>
    /// 배경음악 체인지 메서드
    /// </summary>
    /// <param name="clip"></param>
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
    /// <param name="clip"></param>
    public static void PlayClip(AudioClip clip,AudioResType type = AudioResType.etc)
    {
        //매개 변수 받을 때 enum하나 만들어서. 클립이 무슨 타입의 사운드인지 구분하고 AudioSource output을 이제 지정해주고 프리팹 생성.
        /*작동방식
        PlayClip 호출 > 오브젝트안에 틀어가있는 사운드소스 싱클톤화 후 변수지정 >
        그변수의 지정돼있는 사운드소스 컴포넌트 정보를 변수의 지정 >
        지정되있는 사운드소스 메서드에게 매개변수 정보를 보내면서 호출 >
        오브젝트 안에 클립 사운드 정보 , 음량설정1, 음량설정2를 보내고  Play메서드에서 음악 재생
        */
        SoundSource prefab = Instantiate(instance.prefabSoundSource);
        SoundSource soundSource = prefab.GetComponent<SoundSource>();
        AudioSource audioSource = soundSource.GetComponent<AudioSource>();
        
       

        if(type == AudioResType.sfx)
        {
            audioSource.outputAudioMixerGroup = instance.sfx_;//오디오 소스 output 등록
            soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
        }

        /*프리팹의 사운드 재생
        프리팹에 등록되있는 clip를 사운드 메니져 스트립트의 soundEffectVolume값 soundEffectPitchVariance값으로 설정뒤 재생 
        */
        //soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }
}

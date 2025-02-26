using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField][Range(0f, 1f)] private float musicVolume = 0.145f;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance = 0.156f;
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume = 0.156f;

    private AudioSource audioSource;
    private AudioClip audioClip;

    private SoundSource prefabSoundSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        audioSource.loop = true;

        audioClip = Resources.Load<AudioClip>("Sounds/LobbyBGM/game-8-bit-on-278083");
        prefabSoundSource = Resources.Load<SoundSource>("Prefabs/SoundSource");
    }
    /// <summary>
    /// 시작시 배경음악 재생
    /// </summary>
    private void Start()
    {
        BackGroundMusic(audioClip);
    }
    /// <summary>
    /// 배경음악 체인지 메서드
    /// </summary>
    /// <param name="clip"></param>
    public void BackGroundMusic(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    /// <summary>
    /// 오브젝트 사운드 클립 재생 메서드
    /// </summary>
    /// <param name="clip"></param>
    public static void PlayClip(AudioClip clip)
    {
        /*작동방식
        PlayClip 호출 > 오브젝트안에 틀어가있는 사운드소스 싱클톤화 후 변수지정 >
        그변수의 지정돼있는 사운드소스 컴포넌트 정보를 변수의 지정 >
        지정되있는 사운드소스 메서드에게 매개변수 정보를 보내면서 호출 >
        오브젝트 안에 클립 사운드 정보 , 음량설정1, 음량설정2를 보내고  Play메서드에서 음악 재생
        */
        SoundSource prefab = Instantiate(instance.prefabSoundSource);
        SoundSource soundSource = prefab.GetComponent<SoundSource>();

        /*프리팹의 사운드 재생
        프리팹에 등록되있는 clip를 사운드 메니져 스트립트의 soundEffectVolume값 soundEffectPitchVariance값으로 설정뒤 재생 
        */
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }
}

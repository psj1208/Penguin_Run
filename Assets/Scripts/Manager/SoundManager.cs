using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField][Range(0f, 1f)] private float musicVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;

    private AudioSource audioSource;
    public AudioClip audioClip;

    public SoundSource prefabSoundSource; 
    private void Awake()
    {
        //싱글톤
        if (audioSource == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        audioSource.loop = true;
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
        //호출한곳에서 SoundManager 복제후 복제된 SoundManager의 SoundSource 컴포넌트 정보를 soundSource 변수에 지정
        //사운드를 중복으로 재생하기 위해 SoundManager를 복제후 오브젝트의 클립을 재생하는 과정
        SoundSource prefab = Instantiate(instance.prefabSoundSource);
        SoundSource soundSource = prefab.GetComponent<SoundSource>();

        /*프리팹의 사운드 재생
        프리팹에 등록되있는 clip를 사운드 메니져 스트립트의 soundEffectVolume값 soundEffectPitchVariance값으로 설정뒤 재생 
        */
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }
}

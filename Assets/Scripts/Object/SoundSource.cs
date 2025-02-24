using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource audioSource;

    public void Play(AudioClip clip,float effectVolume,float effectPitchVariance)
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        CancelInvoke();
        audioSource.clip = clip;
        audioSource.volume = effectVolume;
        audioSource.Play();

        Invoke("Deactiv", clip.length + 2);
    }

    public void Deactiv()
    {
        audioSource.Stop();
        Destroy(this.gameObject);
    }
}

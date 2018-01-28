using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource[] AudioSource;

    int lastIdPlayed = 1;

    void Awake()
    {
        MODEL.SOUND_MANAGER = this;
    }

    public void PlayMusic(AudioClip clip)
    {
        AudioSource[0].clip = clip;
        AudioSource[0].loop = true;
        AudioSource[0].Play();        
    }

    public void StopMusic()
    {
        AudioSource[0].Stop();
    }

    public void PlayAudio(AudioClip clip)
    {
        lastIdPlayed = lastIdPlayed < AudioSource.Length ? lastIdPlayed : 1;
         
        AudioSource[lastIdPlayed].clip = clip;
        AudioSource[lastIdPlayed].Play();
        lastIdPlayed++;
    }

    
}

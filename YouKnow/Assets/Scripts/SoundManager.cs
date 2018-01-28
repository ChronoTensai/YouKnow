using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource[] AudioSource;

    int lastIdPlayed = 0;

    void Awake()
    {
        MODEL.SOUND_MANAGER = this;
    }

    public void PlayAudio(AudioClip clip)
    {
        lastIdPlayed = lastIdPlayed < AudioSource.Length ? lastIdPlayed : 0;
         
        AudioSource[lastIdPlayed].clip = clip;
        AudioSource[lastIdPlayed].Play();
        lastIdPlayed++;
    }

    
}

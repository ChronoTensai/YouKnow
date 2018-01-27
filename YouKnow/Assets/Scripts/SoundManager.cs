using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource[] AudioSource;

    int lastIdPlayed = 0;

    void Awake()
    {
        DontDestroyOnLoad(this);
        MODEL.SOUND_MANAGER = this;
    }

    public void PlayAudio(AudioClip clip)
    {
        lastIdPlayed++;
        lastIdPlayed = lastIdPlayed < AudioSource.Length ? lastIdPlayed : 0;       
        AudioSource[lastIdPlayed].clip = clip;
        AudioSource[lastIdPlayed].Play();        
    }

    public void StopAllAudio()
    {
        for (int i = 0; i < AudioSource.Length; i++)
        {
            AudioSource[i].Stop();
        }

    }
}

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
         
        AudioSource[0].clip = clip;
        AudioSource[0].Play();
        lastIdPlayed++;
    }

    public void StopAllAudio()
    {
        for (int i = 0; i < AudioSource.Length; i++)
        {
            AudioSource[i].Stop();
        }
    }
}

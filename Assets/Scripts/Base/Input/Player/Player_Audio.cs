using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio 
{
    private AudioSource audioSource;

    public Player_Audio(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }



    /// <summary>
    /// 播放指定音效
    /// </summary>
    /// <param name="audioClip">音效片段</param>
    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager>
{
    public AudioClip inGameMusic;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        this.StartInGameMusic();
    }

    void StartInGameMusic() {
        this.audioSource.clip = inGameMusic;
        this.audioSource.Play();
    }
}

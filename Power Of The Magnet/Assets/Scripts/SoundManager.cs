﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public static AudioClip Song, Caja, Iman;
    static AudioSource audiosrc;

    private float musicVolume = 0.1f;
    private float sfxVolume = 0.1f;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else {
            Debug.Log("Warning: multiple " + this + " in scene!!");
        }
    }

    // Start is called before the first frame update
    void Start() {
        Song = Resources.Load<AudioClip>("Song");
        Caja = Resources.Load<AudioClip>("Caja");
        Iman = Resources.Load<AudioClip>("Iman");

        audiosrc = GetComponent<AudioSource>();

        PlaySound("Song");
    }
    // Update is called once per frame
    void Update()
    {
        if (!audiosrc.isPlaying) { PlaySound("Song"); }

        
    }

    public void SetVolumeMusic(float vol)
    {
        musicVolume = vol;
    }
    public void SetVolumeSFX(float vol)
    {
        sfxVolume = vol;
    }

    //public static void StopSound() { audiosrc.Stop(); }
    public static void PlaySound(string clip)
    {
        switch (clip) {
            case "Song":
                audiosrc.PlayOneShot(Song);
                break;
        }
    }
}

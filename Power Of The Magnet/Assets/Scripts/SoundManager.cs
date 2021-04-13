using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public static AudioClip Song, Caja, Iman;
    static AudioSource audiosrc;

    private float musicVolume;

    Scene currentScene;
    string sceneName;

    private float introSongTimer;
    private float gameSongTimer;
    private float introSong;
    private float gameSong;

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


        introSong = Song.length;
    }
    // Update is called once per frame
    void Update()
    {
        if (!audiosrc.isPlaying)
        {
            currentScene = SceneManager.GetActiveScene();
            sceneName = currentScene.name;
            if ((sceneName == "MainMenu" || sceneName == "GameOver" || sceneName == "Win"))
            {
                PlaySound("Song");
            }
            //else
            //{
            //    PlaySound("SongGame");
            //}
        }

        audiosrc.volume = musicVolume;

    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }

    //public static void StopSound() { audiosrc.Stop(); }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Song":
                audiosrc.PlayOneShot(Song);
                break;
        }
    }
}

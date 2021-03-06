using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public GameObject Hijo;
    public static AudioClip Song, Iman, Click, Door, Electric, Step;
    private static AudioSource audiosrc;
    private static AudioSource audioSource2;

    private float musicVolume = 0.1f;
    private float sfxVolume = 0.1f;

    void Awake() {

        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Warning: multiple " + this + " in scene!!");
        }
        
        
    }

    // Start is called before the first frame update
    void Start() {
        Song = Resources.Load<AudioClip>("Song");
        Iman = Resources.Load<AudioClip>("Iman");
        Click = Resources.Load<AudioClip>("ClickMenu");
        Door = Resources.Load<AudioClip>("Door");
        Electric = Resources.Load<AudioClip>("Electric");
        Step = Resources.Load<AudioClip>("Step");


        audiosrc = GetComponent<AudioSource>();
        audioSource2 = Hijo.GetComponent<AudioSource>();
        PlaySound("Song");
    }
    // Update is called once per frame
    void Update()
    {
        audioSource2.volume = sfxVolume;

        audiosrc.volume = musicVolume;

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
            case "ClickMenu":
                audioSource2.PlayOneShot(Click);
                break;
            case "Iman":
                audioSource2.PlayOneShot(Iman);
                break;
            case "Door":
                audioSource2.PlayOneShot(Door);
                break;
            case "Electric":
                audioSource2.PlayOneShot(Electric);
                break;
            case "Step":
                audioSource2.PlayOneShot(Step);
                break;
        }
    }
}

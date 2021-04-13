using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SFXSlider;

    private SoundManager SFX_Cont;
    private SoundManager Music_Cont;
    private float Music = 0.1f;
    private float SFX = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
        Music_Cont = GetComponentInChildren<SoundManager>();
        SFX_Cont = GetComponent<SoundManager>();

        if (MusicSlider != null)
        {
            MusicSlider = MusicSlider.GetComponent<Slider>();
            MusicSlider.value = Music;
        }
        else
        {
            Music_Cont.SetVolumeMusic(Music);
        }
        if (SFXSlider != null)
        {
            SFXSlider = SFXSlider.GetComponent<Slider>();
            SFXSlider.value = SFX;
        }
        else
        {
            SFX_Cont.SetVolumeSFX(SFX);
        }
    }

    private void Update()
    {
        if (MusicSlider != null)
        {
            Music = MusicSlider.value;

        }
        if (SFXSlider != null)
        {
            SFX = SFXSlider.value;

        }
    }

    public float GetMusicVol()
    {
        return Music;
    }
    public float GetSFXVol()
    {
        return SFX;
    }
}

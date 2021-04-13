using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private GameObject aMusic;
    private GameObject aSFX;

    private Slider MusicSlider;
    private Slider SFXSlider;

    private SoundManager SFX_Cont;
    private SoundManager Music_Cont;
    private float Music = 0.1f;
    private float SFX = 0.1f;


    // Start is called before the first frame update
    void Start()
    {

        aMusic = GameObject.FindGameObjectWithTag("MusicSlider");
        aSFX = GameObject.FindGameObjectWithTag("SFXSlider");

        Music_Cont = GetComponentInChildren<SoundManager>();
        SFX_Cont = GetComponent<SoundManager>();

        if (aMusic != null)
        {
            MusicSlider = aMusic.GetComponent<Slider>();
            MusicSlider.value = Music;
        }
        else
        {
            Music_Cont.SetVolumeMusic(Music);
        }
        if (aSFX != null)
        {
            SFXSlider = aSFX.GetComponent<Slider>();
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


    //public void SaveValues()
    //{
    //    BinaryWriter writer = new BinaryWriter(File.Open("sound.sav", FileMode.Create));
    //    writer.Write(Music);
    //    writer.Write(SFX);
    //}
}

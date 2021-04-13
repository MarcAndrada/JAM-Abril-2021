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

    private SoundManager Volume_Cont;
    private float Music = 0.1f;
    private float SFX = 0.1f;
    private int tries = 0;

    // Start is called before the first frame update
    void Start()
    {

        aMusic = GameObject.FindGameObjectWithTag("MusicSlider");
        aSFX = GameObject.FindGameObjectWithTag("SFXSlider");
        Volume_Cont = GetComponent<SoundManager>();

        if (aMusic != null)
        {
            MusicSlider = aMusic.GetComponent<Slider>();
            MusicSlider.value = Music;
        }
        else
        {
            Volume_Cont.SetVolumeMusic(Music);
        }
        if (aSFX != null)
        {
            SFXSlider = aSFX.GetComponent<Slider>();
            SFXSlider.value = SFX;
        }
        else
        {
             Volume_Cont.SetVolumeSFX(SFX);
        }
    }

    private void Update()
    {
        if (aMusic == null && tries < 11 || aSFX == null && tries < 11)
        {
            aMusic = GameObject.FindGameObjectWithTag("MusicSlider");
            aSFX = GameObject.FindGameObjectWithTag("SFXSlider");
           if(aMusic != null){
                MusicSlider = aMusic.GetComponent<Slider>();
                MusicSlider.value = Music;
           }else{
                Volume_Cont.SetVolumeMusic(Music);
            }

            if (aSFX != null)
            {
                SFXSlider = aSFX.GetComponent<Slider>();
                SFXSlider.value = SFX;
            }else{
                Volume_Cont.SetVolumeSFX(SFX);
            }
        }

        if (MusicSlider != null)
        {
            Music = MusicSlider.value;

        }
        if (SFXSlider != null)
        {
            SFX = SFXSlider.value;

        }
        if (Volume_Cont != null)
        {
            Volume_Cont.SetVolumeMusic(Music);
            Volume_Cont.SetVolumeSFX(SFX);
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

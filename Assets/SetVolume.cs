using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetMusicLevel (float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void Mute () 
    {
       mixer.SetFloat("MasterVol", Mathf.Log10(0.0001f) * 20); 
    }

    public void SetMasterLevel (float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSoundLevel (float sliderValue)
    {
        mixer.SetFloat("SoundVol", Mathf.Log10(sliderValue) * 20);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private float backgroundFloat, soundEffectsFloat;

    public Slider backgroundSlider, soundEffectsSlider;

    private void Start() 
    {
        this.backgroundFloat = PlayerPrefs.GetFloat("BackgroundPref");
        this.soundEffectsFloat = PlayerPrefs.GetFloat("SoundEffectsPref");

        this.backgroundSlider.value = this.backgroundFloat;
        this.soundEffectsSlider.value = this.soundEffectsFloat;
    }

    public void SaveAudioSettings()
    {            
        PlayerPrefs.SetFloat("BackgroundPref", this.backgroundSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsPref", this.soundEffectsSlider.value);      
    }

    private void FixedUpdate() 
    {
        this.SaveAudioSettings(); 
    }
}

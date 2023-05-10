using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    private float backgroundFloat, soundEffectsFloat;

    public AudioSource backgroundAudio;
    public AudioSource soundEffectsAudio;

    private void Awake() 
    {
        this.backgroundFloat = PlayerPrefs.GetFloat("BackgroundPref");
        this.soundEffectsFloat = PlayerPrefs.GetFloat("SoundEffectsPref");

        this.backgroundAudio.volume = this.backgroundFloat;
        this.soundEffectsAudio.volume = this.soundEffectsFloat;
    }
}

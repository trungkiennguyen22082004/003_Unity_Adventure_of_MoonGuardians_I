using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        instance = this;
        this.source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        this.source.PlayOneShot(_sound);
    }
}

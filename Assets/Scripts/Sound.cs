using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    // Fields
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
    public bool isSFX;
    [Range(0f, 1f), HideInInspector]
    public float volume;
    [Range(0.1f, 3f), HideInInspector]
    public float pitch;
}

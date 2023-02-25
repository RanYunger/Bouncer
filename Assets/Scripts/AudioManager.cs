using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // Fields
    public Sound[] sounds;
    [Range(0f, 1f), HideInInspector]
    public float musicVolume;
    [Range(0f, 1f), HideInInspector]
    public float sfXVolume;

    public static AudioManager instance;

    // Methods
    public void Awake()
    {
        if (instance == null)
            DontDestroyOnLoad(instance = this); // For passing between scenes
        else
        {
            musicVolume = instance.musicVolume;
            sfXVolume = instance.sfXVolume;
        }
    }
    public void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
        }

        AdjustVolume(1f, 1f);
    }
    public void AdjustVolume(float newMusicVolume, float newSFXVolume)
    {
        musicVolume = newMusicVolume;
        sfXVolume = newSFXVolume;

        foreach (Sound s in sounds)
            s.source.volume = s.isSFX ? sfXVolume : musicVolume;
    }
    public void Play(string audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioName);

        if (s != null)
            s.source.Play();
    }
    public void Resume(string audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioName);

        if ((s != null) && (!s.source.isPlaying))
            s.source.UnPause();
    }
    public void PlayOrResume(string audioName)
    {
        if (IsPaused(audioName))
            Resume(audioName);
        else
            Play(audioName);
    }
    public void Pause(string audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioName);

        if ((s != null) && (s.source.isPlaying))
            s.source.Pause();
    }
    public void Stop(string audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioName);

        if (s != null)
            s.source.Stop();
    }
    public bool IsPaused(string audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioName);

        return s != null ? s.source.time > 0f : false;
    }
    public void ResumeAll()
    {
        for (int i = 0; i < sounds.Length; i++)
            Resume(sounds[i].name);
    }
    public void PauseAll()
    {
        for (int i = 0; i < sounds.Length; i++)
            Pause(sounds[i].name);
    }
    public void StopAll()
    {
        for (int i = 0; i < sounds.Length; i++)
            Stop(sounds[i].name);
    }
}

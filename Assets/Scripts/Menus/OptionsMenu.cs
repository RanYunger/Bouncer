using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : Menu
{
    // Fields
    public AudioManager audioManager;
    public GameObject mainMenu;
    public Slider musicVolumeSlider, sFXVolumeSlider;

    // Methods
    public void Awake()
    {
        audioManager = AudioManager.instance;
        musicVolumeSlider.value = audioManager.musicVolume;
        sFXVolumeSlider.value = audioManager.sfXVolume;
    }
    public void Back()
    {
        OnClick();

        audioManager.AdjustVolume(musicVolumeSlider.value, sFXVolumeSlider.value);
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
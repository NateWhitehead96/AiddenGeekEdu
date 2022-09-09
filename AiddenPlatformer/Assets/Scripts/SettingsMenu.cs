using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicVolume;
    public Slider soundVolume;
    public GameObject menuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SoundManager.instance.musicVolume = musicVolume.value; // set the slider value, 0 to 1, to the music
        SoundManager.instance.soundFXVolume = soundVolume.value; // set the slider value to the sound fx
    }

    public void OpenMainMenu()
    {
        menuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}

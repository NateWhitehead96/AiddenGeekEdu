using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public AudioSource coinPickup;
    public AudioSource playerHurt;

    public float musicVolume;
    public float soundFXVolume;

    private void Update()
    {
        // all sound fx volumes equal the sound fx
        coinPickup.volume = soundFXVolume;
        playerHurt.volume = soundFXVolume;
    }
}

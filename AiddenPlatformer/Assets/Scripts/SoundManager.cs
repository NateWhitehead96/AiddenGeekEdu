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
    public AudioSource checkPoint;
    public AudioSource heartPickup;
    public AudioSource explosion;

    public float soundFXVolume; // some volume for our sound effects
    public float musicVolume;

    private void Update()
    {
        coinPickup.volume = soundFXVolume;
        playerHurt.volume = soundFXVolume;
        checkPoint.volume = soundFXVolume;
        heartPickup.volume = soundFXVolume;
        explosion.volume = soundFXVolume;
    }
}

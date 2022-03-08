using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // the contact point for accessing the game manager

    public int LevelsBeaten; // how many levels have we beaten

    public int Lives; // how many lives we got
    private void Awake()
    {
        if(instance != null) // if there is already an instance of the game manager
        {
            Destroy(gameObject); // destroy the one in the scene
        }
        else
        {
            instance = this; // other wise make this version the instance and make sure it doesnt destroy between scenes
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

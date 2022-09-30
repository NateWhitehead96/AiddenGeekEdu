using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMaster : MonoBehaviour
{
    public GameObject settingsCanvas;
    public GameObject playCanvas; // for main menu
    private void Start()
    {
        
        if(settingsCanvas != null) // if there is one
        {
            settingsCanvas.SetActive(false); // hide it
        }
    }

    public void OpenSettings() // open settings, close main menu
    {
        settingsCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ResumeGame() // resume game from pause state
    {
        Time.timeScale = 1; // unpause the game, allows for scripts to update
        gameObject.SetActive(false); // hide whatever canvas this is attached to
    }

    public void ReturnToHub()
    {
        SceneManager.LoadScene("Hubworld"); // load hubworld
    }

    public void ReturnToMainMenu()
    {
        print("Opening Main menu");
        SceneManager.LoadScene("MainMenu"); // load main menu
    }

    public void PlayGame()
    {
        if (PlayerPrefs.HasKey("Lives")) // more to check if we have save data, Lives would be in that
        {
            //ReturnToHub(); // load hubworld
            playCanvas.SetActive(true); // show the play options
        }
        else // no save data
        {
            SceneManager.LoadScene("TutorialLevel"); // load tutorial level
        }
    }

    public void QuitGame()
    {
        Application.Quit(); // this closes the game, but only works in a standalone build (not in editor)
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialLevel"); // load tutorial level
    }
}

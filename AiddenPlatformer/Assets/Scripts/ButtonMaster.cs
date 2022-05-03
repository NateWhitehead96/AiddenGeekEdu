using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMaster : MonoBehaviour
{
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
        //SceneManager.LoadScene("MainMenu"); // load main menu
    }
}

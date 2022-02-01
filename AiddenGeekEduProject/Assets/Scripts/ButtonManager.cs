using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene"); // load our play level
    }

    public void QuitGame()
    {
        Application.Quit(); // to quit the game, only works in a built version not in unity editor
    }
    [SerializeField] // this forces it to be visible in unity's editor
    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu"); // load our main menu
    }
}

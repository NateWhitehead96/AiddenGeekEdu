using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField]public Transform scrollingText;
    public GameObject[] screenThings; // for me all of the other things on screen will be hidden until the text goes off screen
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < screenThings.Length; i++)
        {
            screenThings[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        scrollingText.position = new Vector3(scrollingText.position.x, scrollingText.position.y + 20 * Time.deltaTime);
        if(scrollingText.position.y > 600)
        {
            for (int i = 0; i < screenThings.Length; i++)
            {
                screenThings[i].SetActive(true);
            }
        }
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetGameData()
    {
        PlayerPrefs.DeleteAll(); // this will delete all of the saved data through player prefs
    }
}

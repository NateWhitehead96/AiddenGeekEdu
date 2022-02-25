using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public int LevelToLoad;
    public bool inDoor; // when the player is inside the door

    public bool needsKey; // if the door requires a key to enter
    public int nextLevel; // the level we unlock
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(needsKey == true) // doors at the end of levels will require the key
        {
            if(FindObjectOfType<Player>().hasKey && Input.GetKeyDown(KeyCode.W) && inDoor == true) // player has key
            {
                if(GameManager.instance.LevelsBeaten < nextLevel) // checking to make sure we bump our levels beaten to the right number
                {
                    GameManager.instance.LevelsBeaten = nextLevel;
                }
                SceneManager.LoadScene(LevelToLoad); // loading likely the hubworld
            }
        }
        else // a door in our hub world
        {
            if (inDoor == true && Input.GetKeyDown(KeyCode.W) && GameManager.instance.LevelsBeaten >= LevelToLoad) // only go to next level if we've beaten previous level
            {
                SceneManager.LoadScene(LevelToLoad); // load the level
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // the player is inside the door
        {
            inDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // the player has left the door
        {
            inDoor = false;
        }
    }
}

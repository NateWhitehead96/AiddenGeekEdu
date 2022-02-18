using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public int LevelToLoad;
    public bool inDoor; // when the player is inside the door
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inDoor == true && Input.GetKeyDown(KeyCode.W)) // when we're inside the door, and we hit W
        {
            SceneManager.LoadScene(LevelToLoad); // load the level
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

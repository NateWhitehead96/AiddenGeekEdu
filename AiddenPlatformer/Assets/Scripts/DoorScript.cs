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

    public SpriteRenderer sprite; // the renderer that shows the sprite
    public Sprite OpenDoorSprite; // sprite to switch to when we open the door
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); // make sure sprite renderer is this game objects
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
                GameManager.instance.SaveGame(); // save all of our data when we complete a level
                StartCoroutine(SceneTransition()); // a coroutine to help with playing the scene transition
                
            }
        }
        else // a door in our hub world
        {
            if (inDoor == true && Input.GetKeyDown(KeyCode.W) && GameManager.instance.LevelsBeaten >= LevelToLoad) // only go to next level if we've beaten previous level
            {
                StartCoroutine(SceneTransition());
            }
        }
        
    }

    IEnumerator SceneTransition()
    {
        FindObjectOfType<TransitionCanvas>().anim.SetBool("fade", true); // make the canavs fade in
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(LevelToLoad);// loading likely the hubworld
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // the player is inside the door
        {
            inDoor = true;
        }
        if(needsKey == true && collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().hasKey)
            {
                sprite.sprite = OpenDoorSprite; // set the sprite of the door to the open door sprite
            }
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

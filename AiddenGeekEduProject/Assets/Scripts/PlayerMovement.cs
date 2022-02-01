using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed;

    public float XBounds;
    public float YBounds;

    public float size; // of course the size of the player
    public static int sizeCounter; // a counter for how many fish we eat
    public static float score; // our score that can be accessed by every other script

    private Animator animator; // the animation controller

    public GameObject fishBones; // the fishy bones
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // makes sure the animator is the one from this game object
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(size, size, 1); // this will continually make sure we're the right size

        if (Input.GetKey(KeyCode.W) && transform.position.y < YBounds) // moving up
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            animator.SetInteger("Direction", 1); // sets the animation
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y > -YBounds) // moving down
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
            animator.SetInteger("Direction", 3); // sets the animation
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x > -XBounds) // moving left
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            animator.SetInteger("Direction", 4); // sets the animation
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < XBounds) // moving right
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            animator.SetInteger("Direction", 2); // sets the animation
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish")) // check to see if what we're colliding with is tagged at fish
        {

            EnemyMovement fish = collision.gameObject.GetComponent<EnemyMovement>(); // we're gaining access to the fish we collide with

            if(size > fish.size) // we're bigger than the other fish
            {

                score += fish.size * 5; // add to our score (fish size * 5)
                GameObject newBones = Instantiate(fishBones, transform.position, transform.rotation); // spawn the dead fish out our bum bum as a new local variable
                newBones.GetComponent<Transform>().localScale = new Vector3(fish.size / 3, fish.size / 3, 1); // set the fishbones size to be the eaten fish size

                Destroy(collision.gameObject); // destroy the fish

                sizeCounter++; // increase the counter
                if(sizeCounter >= 5)
                {
                    size += 0.3f; // increase size
                    sizeCounter = 0; // reset the size counter
                }

                print("Ate the fish :)"); // get the score
            }
            else
            {
                SceneManager.LoadScene("GameOver"); // we die, open gave over scene
            }
            // else we die but we wont do that just yet
        }
    }
}

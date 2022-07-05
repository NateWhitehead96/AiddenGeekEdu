using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed; // moving player
    public float jumpForce; // how high we jump
    public Rigidbody2D rb; // allowing us to add forces to player
    public SpriteRenderer sprite; // help flip the sprite to the right direction
    public Transform Checkpoint; // will be our current respawn point

    private Animator animator; // animations controller
    // these bools will help us transition between animations
    public bool walking;
    public bool jumping;
    public bool climbing;
    public bool swimming;

    public int Health = 3;
    //public int Coins; Coins will now be stored in Game Manager
    public int Score;
    public float levelTimer;

    public bool hasKey; // player collected key of the level

    public GameObject PauseCanvas; // access to pause canvas

    public ParticleSystem dust;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; // to make sure the game isn't paused when we open a new scene
        PauseCanvas.SetActive(false); // make sure pause canvas is hidden to start
        rb = GetComponent<Rigidbody2D>(); // links the rb variable to the gameobjects rigidbody
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Checkpoint = transform; // our first checkpoint aka spawn point will be whatever the players initial position is
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer -= Time.deltaTime; // goes down
        if (Input.GetKey(KeyCode.D)) // going right with the D key
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            sprite.flipX = false; // it make sure it faces right 
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.D)) // releasing the D key
        {
            walking = false;
        }
        if (Input.GetKey(KeyCode.A)) // going left with the A key
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            sprite.flipX = true; // make the character face left
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            walking = false;
        }

        animator.SetBool("isWalking", walking); // switch to walking animation
        animator.SetBool("isJumping", jumping); // switch to jumping animation
        animator.SetBool("isClimbing", climbing); // switch to the climb animation
        // -------------- Jumping input -------------- //
        if (swimming == true)
        { 
            jumping = false; //if we're in water we can jump unlimited amounts
        } 
        if (Input.GetKeyDown(KeyCode.Space) && jumping == false)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); // jump function
            jumping = true;
        }
        if(rb.velocity.y > 5) // cap jump velocity
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
        }
        // ------------- Climbing ladders input -------------- //
        if(climbing == true && Input.GetKey(KeyCode.W)) // we're on a ladder going up
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        if(climbing == true && Input.GetKey(KeyCode.S)) // on the ladder going down
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
        }

        // --------------- Pause Input ------------------- //
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseCanvas.activeInHierarchy) // is the pause canvas active
            {
                Time.timeScale = 1;
                PauseCanvas.SetActive(false);
            }
            else // pause canvas isn't active
            {
                Time.timeScale = 0;
                PauseCanvas.SetActive(true);
            }
        }

        if(walking == true && jumping == false && swimming == false && climbing == false)
        {
            dust.Play();
        }
        if(walking == false || jumping == true || swimming == true || climbing == true)
        {
            dust.Stop();
        }
    }

    public void HurtPlayer() // the damage function that enemies will use
    {
        StartCoroutine(HurtAnimation());
    }
    IEnumerator HurtAnimation()
    {
        animator.SetBool("isHurt", true); // play animation
        SoundManager.instance.playerHurt.Play(); // play the hurt sound
        Health--; // take damage
        if(Health <= 0)
        {
            GameManager.instance.Lives--; // subtract a life
            // check to see if lives is less than 0
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level4"))
            {
                FindObjectOfType<AutoscrollingPlatform>().transform.position = FindObjectOfType<AutoscrollingPlatform>().startPos;
                FindObjectOfType<AutoscrollingPlatform>().playerOn = false;
                FindObjectOfType<AutoscrollingPlatform>().currentPoint = Checkpoint.gameObject.GetComponent<Level4Checkpoint>().platformPoint;
                transform.parent = null;
            }
            transform.position = Checkpoint.position; // reset to last checkpoint
            Health = 3; // reset health back to 3
        }
        yield return new WaitForSeconds(1);
        animator.SetBool("isHurt", false); // turn hurt animation off
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false; // when we collide with anything, we're more or less grounded
        if (collision.gameObject.GetComponent<FloatingPlatform>())
        {
            transform.parent = collision.gameObject.transform; // we parent player to the platform
        }
        if (collision.gameObject.GetComponent<AutoscrollingPlatform>())
        {
            transform.parent = collision.gameObject.transform;
            collision.gameObject.GetComponent<AutoscrollingPlatform>().playerOn = true; // tell it player is on
        }
        if (collision.gameObject.CompareTag("Hazard"))
        {
            switch (Health)
            {
                case 1: // if we have 1 health hurt player 1 time
                    HurtPlayer();
                    break;
                case 2: // if we have 2 health hurt player 2 times
                    HurtPlayer();
                    HurtPlayer();
                    break;
                case 3: // if we have 3 health hurt player 3 times
                    HurtPlayer();
                    HurtPlayer();
                    HurtPlayer();
                    break;
                default:
                    print("Hit default break");
                    break;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<FloatingPlatform>())
        {
            transform.parent = null; // make the player unparented
        }
        if (collision.gameObject.GetComponent<AutoscrollingPlatform>())
        {
            transform.parent = null;
            //collision.gameObject.GetComponent<AutoscrollingPlatform>().playerOn = false; // tell it player is off
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Head")) // if we're jumping on an enemy head
        {
            collision.transform.GetComponentInParent<GroundEnemy>().EnemyDie(); // show the spider dead
            Destroy(collision.gameObject);
            Score += 100;
            rb.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
            //Destroy(collision.transform.parent.gameObject); // destroying the spider, which is the parent of the head
        }
        if (collision.gameObject.CompareTag("Deathplane"))
        {
            transform.position = Checkpoint.position; // reset our position
            GameManager.instance.Lives--;
            Health = 3;
        }
        if (collision.gameObject.CompareTag("Heart")) 
        {
            Score += 150;
            Health++; // add to health
            if(Health > 3) // to make sure the health is capped
            {
                Health = 3;
            }
            Destroy(collision.gameObject); // destroy heart
        }
        if (collision.gameObject.CompareTag("Ladder")) // on the ladder
        {
            climbing = true; // we're now able to climb
            rb.gravityScale = 0; // disable gravity while on ladder
        }
        if(collision.gameObject.name == "Key" && hasKey == false) // make sure we dont get more points than 1 time
        {
            Score += 200;
            hasKey = true;
            //Destroy(collision.gameObject); // make sure we dont destroy the key
        }
        if (collision.gameObject.CompareTag("Checkpoint")) // walking through checkpoint
        {
            Checkpoint = collision.gameObject.transform; // sets the checkpoint to the position of the new checkpoint
        }
        if (collision.gameObject.CompareTag("Shell"))
        {
            collision.transform.GetComponentInParent<Snail>().HideInShell(); // the shell is a child
            Score += 100;
            rb.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("Water")) // colliding with water
        {
            swimming = true;
            rb.gravityScale = 0.3f; // lower gravity
            jumpForce = 3; // lower jump power
            moveSpeed = 3; // lower move speed;
            //rb.velocity = Vector2.zero;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder")) // left the ladder
        {
            climbing = false;
            rb.gravityScale = 1;
        }
        if (collision.gameObject.CompareTag("Water")) // colliding with water
        {
            swimming = false;
            rb.gravityScale = 1; // bring back gravity
            jumpForce = 5; // bring back jump power
            moveSpeed = 5; // bring back move speed
        }
    }
}

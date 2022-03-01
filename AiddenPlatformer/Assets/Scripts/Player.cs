using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int Health = 3;
    public int Coins;

    public bool hasKey; // player collected key of the level
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // links the rb variable to the gameobjects rigidbody
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.Space) && jumping == false)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); // jump function
            jumping = true;
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
    }

    public void HurtPlayer() // the damage function that enemies will use
    {
        StartCoroutine(HurtAnimation());
    }
    IEnumerator HurtAnimation()
    {
        animator.SetBool("isHurt", true); // play animation
        Health--; // take damage
        yield return new WaitForSeconds(1);
        animator.SetBool("isHurt", false); // turn hurt animation off
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false; // when we collide with anything, we're more or less grounded
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Head")) // if we're jumping on an enemy head
        {
            Destroy(collision.transform.parent.gameObject); // destroying the spider, which is the parent of the head
        }
        if (collision.gameObject.CompareTag("Deathplane"))
        {
            transform.position = Checkpoint.position; // reset our position
        }
        if(collision.gameObject.CompareTag("Heart")) 
        {
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
        if(collision.gameObject.name == "Key")
        {
            hasKey = true;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder")) // left the ladder
        {
            climbing = false;
            rb.gravityScale = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed; // moving player
    public float jumpForce; // how high we jump
    public Rigidbody2D rb; // allowing us to add forces to player
    public SpriteRenderer sprite; // help flip the sprite to the right direction

    private Animator animator; // animations controller
    // these bools will help us transition between animations
    public bool walking;
    public bool jumping;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); // jump function
            jumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false; // when we collide with anything, we're more or less grounded
    }
}
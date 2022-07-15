using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossBrain // state machine to know what the fists should do
{
    Resetting, // its landed and going back to start position
    Tracking // its following the player
}

public class BossFist : MonoBehaviour
{
    public BossBrain brain; // to know what the fist is doing
    public Vector3 startPosition; // to know where fist started
    public Transform player; // to know player position

    public float timer; // to help with tracking
    public bool attacking; // to help with knowing if the fists deal damage
    public bool stunned; // to help keep fists on the ground
    public Rigidbody2D rb; // to help with manipulating its gravity
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // link the rigidbody
        startPosition = transform.position; // set our start pos to be our original position
    }

    // Update is called once per frame
    void Update()
    {
        // ---- Resetting ---- //
        if(brain == BossBrain.Resetting)
        {
            rb.velocity = Vector2.zero; // reset the velocity of the fist
            transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime); // move fist back to start
            float distance = Vector3.Distance(transform.position, startPosition); // calculate distance
            if(distance <= 0.1f) // when fist is back at start position
            {
                brain = BossBrain.Tracking; // flip brain to now track player
            }
        }
        // ---- Tracking ---- //
        if(brain == BossBrain.Tracking)
        {
            if(attacking == false && stunned == false) // if the fist has not attacked yet
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y), 2 * Time.deltaTime);
            }
            if(timer >= 3) // after 3 seconds
            {
                attacking = true; // the fist is angry and is attacking
                rb.gravityScale = 10; // slams down using gravity
                timer = 0; // reset timer
            }
            timer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() && attacking == true)
        {
            collision.gameObject.GetComponent<Player>().HurtPlayer(); // deal the damage
            rb.gravityScale = 0;
            attacking = false;
            StartCoroutine(StunFist());
            timer = 0;
        }
        if(collision.gameObject.name == "Ground" || collision.gameObject.name.Contains("Pushable"))
        {
            rb.gravityScale = 0;
            attacking = false;
            StartCoroutine(StunFist());
            timer = 0;
        }
    }

    IEnumerator StunFist()
    {
        stunned = true;
        yield return new WaitForSeconds(1);
        brain = BossBrain.Resetting;
        stunned = false;
    }
}

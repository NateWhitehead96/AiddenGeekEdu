using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlooperBehaviour
{
    wander,
    chase
}

public class Blooper : MonoBehaviour
{
    // some things we need
    // knowledge of the player for distance checking
    public Transform player;
    // the rigidbody of the blooper
    public Rigidbody2D rb;
    // vertical and horizontal force
    public float verticalForce;
    public float horizontalForce;
    // state machine
    public BlooperBehaviour behaviour;
    // timer for movement
    public float timer;
    // sprite renderer for flipping
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(behaviour == BlooperBehaviour.wander)
        {
            if(timer >= 1.5f)
            {
                rb.AddForce(Vector2.up * verticalForce, ForceMode2D.Impulse);
                int randDirection = Random.Range(0, 2); // random between 0 and 1
                if(randDirection == 0) // move to the left
                {
                    rb.AddForce(Vector2.left * horizontalForce, ForceMode2D.Impulse);
                    sprite.flipX = false;
                }
                if(randDirection == 1) // move to the right
                {
                    rb.AddForce(Vector2.right * horizontalForce, ForceMode2D.Impulse);
                    sprite.flipX = true;
                }
                timer = 0; // reset timer
            }
            timer += Time.deltaTime;
            // local variable
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if(distanceToPlayer <= 12 && player.GetComponent<Player>().swimming == true)
            {
                behaviour = BlooperBehaviour.chase;
                rb.gravityScale = 0;
            }
        }
        if(behaviour == BlooperBehaviour.chase)
        {
            rb.velocity = Vector2.zero;
            if(transform.position.x > player.position.x)
                sprite.flipX = false;
            if (transform.position.x < player.position.x)
                sprite.flipX = true;
            // move towards player if close
            transform.position = Vector3.MoveTowards(transform.position, player.position, horizontalForce * Time.deltaTime);
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer >= 12)
            {
                behaviour = BlooperBehaviour.wander;
                rb.gravityScale = 0.2f;
            }
        }

        // if the player is being chased and jumps out of the water, reset the squid
        if(player.GetComponent<Player>().swimming == false)
        {
            behaviour = BlooperBehaviour.wander;
            rb.gravityScale = 0.2f;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().HurtPlayer();
        }
    }
}

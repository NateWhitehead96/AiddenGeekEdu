using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField]
    public int moveSpeed; // how fast the enemy travels
    public int direction = -1; // which way its gunna go
    public float leftBounds; // how far left it can travel
    public float rightBounds; // how far right it can travel

    public SpriteRenderer sprite; // allows us to flip the sprite based on direction

    public Sprite deathSprite; // the sprite to show when the enemy dies
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); // this is just to make sure the sprite component is the sprite on this gameobject (optional)
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + moveSpeed * direction * Time.deltaTime, transform.position.y); // movement

        if(transform.localPosition.x > rightBounds)
        {
            direction = -1;
            sprite.flipX = false;
        }
        if(transform.localPosition.x < leftBounds)
        {
            direction = 1;
            sprite.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().HurtPlayer(); // play the hurt player stuff
        }
        if (collision.gameObject.GetComponent<GroundEnemy>()) // when a ground enemy hits another flip it
        {
            if (direction == 1)
            {
                direction = -1;
                sprite.flipX = false;
            }
            else
            {
                direction = 1;
                sprite.flipX = true;
            }
        }
    }

    public void EnemyDie()
    {
        GetComponent<Animator>().enabled = false; // disable the movement animation
        GetComponent<BoxCollider2D>().enabled = false;
        sprite.sprite = deathSprite;
        moveSpeed = 0;
    }
}

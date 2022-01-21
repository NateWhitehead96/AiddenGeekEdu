using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int moveSpeed;

    public float XBounds;
    public float YBounds;

    public float size; // of course the size of the player
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(size, size, 1); // this will continually make sure we're the right size

        if (Input.GetKey(KeyCode.W) && transform.position.y < YBounds) // moving up
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y > -YBounds) // moving down
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x > -XBounds) // moving left
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < XBounds) // moving right
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish")) // check to see if what we're colliding with is tagged at fish
        {
            if(size > collision.gameObject.GetComponent<EnemyMovement>().size) // we're bigger than the other fish
            {
                Destroy(collision.gameObject); // destroy the fish
                size += 0.1f; // increase size
                print("Ate the fish :)"); // get the score
            }
            // else we die but we wont do that just yet
        }
    }
}

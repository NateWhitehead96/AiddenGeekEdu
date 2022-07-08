using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSwoop : MonoBehaviour
{
    public bool seePlayer; // to know if our bird see the player
    public Vector3 rotatePoint; // the point we make our bird rotate around
    public float leftBounds, rightBound; // the patrol points
    public int direction; // what direction bird is flying
    [SerializeField]public Transform rayPosition; // where the raycast starts
    public LayerMask layer; // a layer the player will also have to make the raycast work
    float timer;
    public float swoopTime;
    Vector2 hitDirection; // the direction the raycast is fired
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // -------- bird patrol movement ------- //
        if(seePlayer == false)
        {
            transform.position = new Vector3(transform.position.x + direction * Time.deltaTime, transform.position.y); // moves birb
        }
        if(transform.position.x > rightBound) // too far right
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
            direction = -1; // move us left
        }
        if(transform.position.x < leftBounds) // too far left
        {
            transform.localScale = new Vector3(0.3f, -0.3f, 1);
            direction = 1; // move us right
        }

        // ------ Swoop Movement ------ //
        if(seePlayer == true)
        {
            transform.RotateAround(rotatePoint, Vector3.forward, 200 * direction * Time.deltaTime); // rotates us around the point
            timer += Time.deltaTime; // start to increase a small timer
            if(timer >= swoopTime) // when we've swooped long enough
            {
                timer = 0; // reset time
                seePlayer = false; // dont see player anymore
                transform.rotation = Quaternion.Euler(0, 0, -90); // because I rotate my bird I want to put it back to its original rotation
                rotatePoint = Vector3.zero; // reset rotate point
            }
        }

        // ------ Raycast stuff ------ //
        if(direction == -1)
        {
            hitDirection = new Vector2(transform.position.x - 2, transform.position.y - 2) - new Vector2(transform.position.x, transform.position.y);
        }
        if (direction == 1)
        {
            hitDirection = new Vector2(transform.position.x + 2, transform.position.y - 2) - new Vector2(transform.position.x, transform.position.y);
        }

        RaycastHit2D hit = Physics2D.Raycast(rayPosition.position, hitDirection, 10, layer); // creating the raycast
        if (hit.collider == null) return; // stop firing laser if does not collide with anything
        if (hit.collider.GetComponent<Player>())
        {
            if(seePlayer == false)
            {
                seePlayer = true; // we now see player for the swoop
                rotatePoint = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 2); // set a point
            }
        }
    }
}

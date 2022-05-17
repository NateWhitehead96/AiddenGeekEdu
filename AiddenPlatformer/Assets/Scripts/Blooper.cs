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
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
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
                }
                if(randDirection == 1) // move to the right
                {
                    rb.AddForce(Vector2.right * horizontalForce, ForceMode2D.Impulse);
                }
                timer = 0; // reset timer
            }
            timer += Time.deltaTime;
            // local variable
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if(distanceToPlayer <= 12)
            {
                behaviour = BlooperBehaviour.chase;
            }
        }
        if(behaviour == BlooperBehaviour.chase)
        {   // move towards player if close
            transform.position = Vector3.MoveTowards(transform.position, player.position, horizontalForce * Time.deltaTime);
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer >= 12)
            {
                behaviour = BlooperBehaviour.wander;
            }
        }
    }
}

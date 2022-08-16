using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject[] healthChunks; // hold all of our health chunks
    public int health; // health
    public BoxCollider2D hitBox; // the collider around the gem
    // shake variable
    public float shakeDuration; // how long to shake
    public float shakeMagnitude; // how aggressive the shake will be
    public float damping; // how much it should slow down by
    public Vector3 initialPosition; // start position for the head
    // phase 2 variables
    bool phaseActive = false; // to know if we're in phase 2
    public float leftBounds, rightBounds; // how far left and right the head can move
    public float speed; // how fast the head moves
    int direction = 1; // direction the head will go in
    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>(); // make sure the collider is set
        initialPosition = transform.position; // set initial pos
    }

    // Update is called once per frame
    void Update()
    {
        if(phaseActive == true) // phase 2 was activated
        {
            transform.position = new Vector3(transform.position.x + direction * speed * Time.deltaTime, transform.position.y); // get the head moving
            if(transform.position.x < leftBounds)
            {
                direction = 1;
            }
            if(transform.position.x > rightBounds)
            {
                direction = -1;
            }
        }

        if(shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude; // add random movement to the shake
            shakeDuration -= Time.deltaTime * damping; // slow down the shake time
        }

        if(health == 4)
        {
            healthChunks[0].SetActive(false);
        }
        if (health == 3)
        {
            healthChunks[1].SetActive(false);
        }
        if (health == 2)
        {
            healthChunks[2].SetActive(false);
        }
        if (health == 1)
        {
            healthChunks[3].SetActive(false);
        }
        if (health == 0)
        {
            if (phaseActive == true)
            {
                FindObjectOfType<BossTrigger>().BossDead();
            }
            else
            {
                //healthChunks[4].SetActive(false);
                // for now we can destroy the boss, later we will make it either change forms or activate a cutscene
                //Destroy(gameObject);
                phaseActive = true; // activate the 2nd phase
                for (int i = 0; i < healthChunks.Length; i++) // restore all health bars
                {
                    healthChunks[i].SetActive(true);
                }
                health = 5; // restore health to 5
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().jumping = true; // hopefully activate the animation and lock us from double jumping
            if (collision.gameObject.transform.position.x < transform.position.x) // left hitting gem
            {
                collision.gameObject.GetComponent<Player>().rb.AddForce(new Vector2(-5, 5), ForceMode2D.Impulse); // add force to our player
            }
            if (collision.gameObject.transform.position.x > transform.position.x) // right hitting gem
            {
                collision.gameObject.GetComponent<Player>().rb.AddForce(new Vector2(5, 5), ForceMode2D.Impulse); // add force to our player
            }
            StartCoroutine(DamageBoss());
        }
    }

    IEnumerator DamageBoss()
    {
        shakeDuration = 1; // set how long the head shakes
        health--; // decrease health
        hitBox.enabled = false; // so we cant hit the gem more than once
        yield return new WaitForSeconds(2);
        hitBox.enabled = true; // bring back the hitbox for the gem
    }
}

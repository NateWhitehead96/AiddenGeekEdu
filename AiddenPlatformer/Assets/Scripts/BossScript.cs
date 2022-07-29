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
    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>(); // make sure the collider is set
        initialPosition = transform.position; // set initial pos
    }

    // Update is called once per frame
    void Update()
    {
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
            healthChunks[4].SetActive(false);
            // for now we can destroy the boss, later we will make it either change forms or activate a cutscene
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
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

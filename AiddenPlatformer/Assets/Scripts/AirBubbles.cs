using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubbles : MonoBehaviour
{
    public float moveSpeed = 3;
    public float timer;

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime); // move the bubble up/forward
        if(timer >= 3)
        {
            Destroy(gameObject); // if the bubble has been around for 3 seconds, destroy it. We dont need to reset timer in this case
        }
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BreathMeter>())
        {
            BreathMeter playerBreath = collision.gameObject.GetComponent<BreathMeter>(); // storing the collisions component into this variable
            playerBreath.currentBreath = playerBreath.maxBreath;
            Destroy(gameObject); // destroy bubble
        }
    }
}

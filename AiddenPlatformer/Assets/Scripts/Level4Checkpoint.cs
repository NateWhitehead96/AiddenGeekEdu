using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Checkpoint : Checkpoint
{
    public AutoscrollingPlatform platform;
    public int platformPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            effect.Play();
            activated = true;
            collision.gameObject.GetComponent<Player>().Health = 3; // restore player to full health
            platform.startPos = platform.movePoints[platform.currentPoint].position; // save the new position as whatever the platform is moving to
            platformPoint = platform.currentPoint; // store what that current point is
        }
    }
}

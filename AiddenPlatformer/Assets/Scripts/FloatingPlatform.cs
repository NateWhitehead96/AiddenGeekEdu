using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public float moveSpeed;
    public float MaxDistance;
    public float MinDistance;
    public int direction;
    public float stopTimer;
    public bool droppingOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(droppingOff == false) // for moving the platform
        {
            transform.position = new Vector3(transform.position.x + direction * moveSpeed * Time.deltaTime, transform.position.y);
        }
        if(transform.position.x >= MaxDistance) // when it hits the right side 
        {
            direction = -1;
            droppingOff = true;
        }
        if(transform.position.x <= MinDistance) // when it hits the left side
        {
            direction = 1;
            droppingOff = true;
        }
        if(droppingOff == true) // we've hit one of our sides. start counting and after 2 seconds flip to be back to moving
        {
            stopTimer += Time.deltaTime;
            if(stopTimer >= 2)
            {
                droppingOff = false;
                stopTimer = 0;
            }
        }
    }
}

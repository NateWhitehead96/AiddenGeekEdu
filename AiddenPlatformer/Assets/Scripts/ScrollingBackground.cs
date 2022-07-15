using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y);
        if(transform.localPosition.x <= -20.5f) // when the piece goes off screen left
        {
            transform.localPosition = new Vector3(20.5f, transform.localPosition.y); // reset it offscreen to the right
        }
    }
}

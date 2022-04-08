using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisiblock : MonoBehaviour
{
    public SpriteRenderer sprite; // thing that renders the block
    public BoxCollider2D box; // this is the collider around the box
    public float timer;
    public float VisibiltyTime; // how long is it going to be visible
    public bool invisible; 
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= VisibiltyTime)
        {
            if(invisible == true) // if the block is invisible
            {
                invisible = false; // flip bool
                sprite.enabled = true; // show sprite
                box.enabled = true; // have collider work
            }
            else if(invisible == false) // if block is not invisible
            {
                invisible = true;
                sprite.enabled = false; // hide sprite
                box.enabled = false; // hide collider
            }

            timer = 0; // reset timer
        }
    }
}

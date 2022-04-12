using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : GroundEnemy 
{
    public Animator anim; // handle our animations
    public float timer;
    public bool inShell;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + moveSpeed * direction * Time.deltaTime, transform.position.y); // movement

        if (transform.position.x > rightBounds)
        {
            direction = -1;
            sprite.flipX = false;
        }
        if (transform.position.x < leftBounds)
        {
            direction = 1;
            sprite.flipX = true;
        }
        if (inShell)
        {
            timer += Time.deltaTime; // count up the timer
            if(timer >= 3)
            {
                anim.SetBool("Hide", false);
                inShell = false;
                timer = 0;
                moveSpeed = 1;
            }
        }
    }

    public void HideInShell()
    {
        anim.SetBool("Hide", true);
        inShell = true;
        moveSpeed = 0;
        timer = 0;
    }
}

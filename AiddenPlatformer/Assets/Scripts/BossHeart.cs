using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeart : MonoBehaviour
{
    public Rigidbody2D rb;
    public ItemMovement item;
    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<ItemMovement>();
        item.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchHeart()
    {
        float randomX = Random.Range(-5, 5); // random x force
        rb.AddForce(new Vector2(randomX, 4), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            rb.gravityScale = 0;
            item.enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.gravityScale = 0;
        item.enabled = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
        rb.velocity = Vector2.zero;
    }
}

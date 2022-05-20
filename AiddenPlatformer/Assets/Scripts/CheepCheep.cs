using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheepCheep : MonoBehaviour
{
    public bool upDown; // to know if the cheep is going up and down or not
    public float leftBounds, rightBounds; // to know how far it can travel left and right
    public float upBounds, downBounds; // to know how far up and down it can travel

    public int direction;
    public int moveSpeed;

    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(upDown == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * direction * Time.deltaTime);
            if(transform.position.y >= upBounds)
            {
                direction = -1;
            }
            if(transform.position.y <= downBounds)
            {
                direction = 1;
            }
        }
        if (upDown == false)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * direction * Time.deltaTime, transform.position.y);
            if (transform.position.x >= rightBounds)
            {
                direction = -1;
            }
            if (transform.position.x <= leftBounds)
            {
                direction = 1;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().HurtPlayer();
        }
    }
}

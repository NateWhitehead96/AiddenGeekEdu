using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float moveSpeed;
    public int direction = 1;
    public float topBounds;
    public float botBounds;

    public bool isHeart; // a bool to check if the item this script is attached to is heart or not (breathing effect)
    // Start is called before the first frame update
    void Start()
    {
        topBounds = transform.position.y + 0.5f; // top bounds
        botBounds = transform.position.y - 0.5f; // bot bounds
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeart)
        {
            transform.localScale = new Vector3(transform.localScale.x + direction * Time.deltaTime, transform.localScale.y + direction * Time.deltaTime);
            if (transform.localScale.x > 1.5f)
            {
                direction = -1;
            }
            if (transform.localScale.x < 1)
            {
                direction = 1;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * direction * Time.deltaTime); // move the coin

            if (transform.position.y > topBounds)
            {
                direction = -1;
            }
            if (transform.position.y < botBounds)
            {
                direction = 1;
            }
        }
    }
}

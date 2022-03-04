using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float moveSpeed;
    public int direction = 1;
    public float topBounds;
    public float botBounds;
    // Start is called before the first frame update
    void Start()
    {
        topBounds = transform.position.y + 0.5f; // top bounds
        botBounds = transform.position.y - 0.5f; // bot bounds
    }

    // Update is called once per frame
    void Update()
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
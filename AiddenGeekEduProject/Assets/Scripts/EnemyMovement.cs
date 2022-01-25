using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction // which direction the enemy will move
{
    Up,
    Down,
    Left,
    Right
}
public class EnemyMovement : MonoBehaviour
{
    public int moveSpeed;
    public Direction direction;

    public float size; // the enemy size

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        size = Random.Range(1, 8); // find a random size
        transform.localScale = new Vector3(size, size, 1); // apply that size
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == Direction.Up) // our enemy will move up
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            animator.SetInteger("Direction", 1);
        }
        if(direction == Direction.Down) // enemy move down
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
            animator.SetInteger("Direction", 3);
        }
        if(direction == Direction.Left) // enemy move left
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            animator.SetInteger("Direction", 4);
        }
        if(direction == Direction.Right) // enemy move right
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            animator.SetInteger("Direction", 2);
        }

        if(transform.position.x > 12 || transform.position.x < -12 || transform.position.y > 6 || transform.position.y < -6) // bounds check to make sure
            // enemy is within camera view
        {
            Destroy(gameObject);
        }
    }
}

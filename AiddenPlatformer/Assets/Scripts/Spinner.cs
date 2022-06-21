using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float moveSpeed; // how fast it rises
    public Rigidbody2D rb; // access to rigidbody for gravity
    public float RestingYPosition; // top position it starts at and will return to after falling
    public bool fellDown; // to know if it has fallen or not
    public Transform raycastPosition; // the raycast spot
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastPosition.position, Vector3.down, Mathf.Infinity); // create the raycast and store it in hit
        Debug.DrawRay(raycastPosition.position, Vector3.down * 1000); // should draw a debug ray to match the above ray we cast
        if (hit.collider == null) return;
        if(hit.collider.gameObject.CompareTag("Player") && fellDown == false) // raycast see player and hasnt fallen yet
        {
            rb.gravityScale = 15;
            fellDown = true;
        }
        if(transform.position.y < RestingYPosition && fellDown == true) // return the spinner back to its resting pos
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }
        if(transform.position.y >= RestingYPosition)
        {
            fellDown = false; // once it gets to the resting y pos flip bool
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.gravityScale = 0; // reset gravity
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().HurtPlayer(); // hurt player if they touch the spinner
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        rb.gravityScale = 0;
        fellDown = true;
    }
}

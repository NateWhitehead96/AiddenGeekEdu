using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFollow : MonoBehaviour
{
    public Vector3 offset; // the offset position from the player
    public Transform player;
    public bool collected;
    public DoorScript exitdoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collected)
        {
            Vector3 followPosition = player.position + offset; // create a new position for the key to always try to go to
            transform.position = Vector3.Lerp(transform.position, followPosition, Time.deltaTime);
            if (exitdoor.inDoor) // when the player enters the door, "use up key"
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.transform; // set player transfrom dynamicly
            collected = true;
            GetComponent<ItemMovement>().enabled = false; // disable other movements
        }
    }
}

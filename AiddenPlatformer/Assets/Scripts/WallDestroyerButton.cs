using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyerButton : MonoBehaviour
{
    public GameObject wallToDestroy; // the thing we want to destroy
    private SpriteRenderer sprite; // access to change the sprite
    public Sprite pressedImage; // the pressed sprite we'll switch to
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Destroy(wallToDestroy); // destroy the wall
            sprite.sprite = pressedImage; // switch the sprite to be the pressed image of the button
        }
    }
}

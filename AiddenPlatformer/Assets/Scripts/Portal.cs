using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform portalPos; // the end position when the player touches the portal
    public Sprite downSprite; // the sprite we want our portal button to turn into
    public Sprite upSprite;
    public SpriteRenderer spriteRender; // access to the sprite renderer

    public bool activated; // if the player has used the portal
    public float timer; // for deactivating the button

    public bool canSpin; // to just know if this can spin or not
    public float rotateSpeed; // how fast it rotates
    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpin) // if this thing can spin
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime); // rotate clockwise
        }
        if (activated) // if the player has teleported
        {
            timer += Time.deltaTime;
            if(timer >= 3) // after 3 seconds deactivate, change sprite back to up, reset timer
            {
                activated = false;
                spriteRender.sprite = upSprite;
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated) // if the player is touching and it's not activated
        {
            spriteRender.sprite = downSprite;
            activated = true;
            collision.gameObject.transform.position = portalPos.position;
        }
    }
}

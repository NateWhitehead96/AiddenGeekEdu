using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathplane : MonoBehaviour
{
    public AutoscrollingPlatform platform;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.transform.position = collision.gameObject.GetComponent<Player>().Checkpoint.position;
            GameManager.instance.Lives--;
            collision.gameObject.GetComponent<Player>().Health = 3;
            platform.transform.position = platform.startPos;
            platform.playerOn = false;
        }
    }
}

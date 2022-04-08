using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public ParticleSystem effect;

    public bool activated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activated)
        {
            effect.Play();
            activated = true;
            collision.gameObject.GetComponent<Player>().Health = 3; // restore player to full health
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    public GameObject displayCanvas; // the text display for our sign

    private void Start()
    {
        displayCanvas.SetActive(false); // hide the canvas on start
    }
    private void OnTriggerEnter2D(Collider2D collision) // show the text when player touches sign
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            displayCanvas.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // hide the text when player leaves the sign
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            displayCanvas.SetActive(false);
        }
    }
}

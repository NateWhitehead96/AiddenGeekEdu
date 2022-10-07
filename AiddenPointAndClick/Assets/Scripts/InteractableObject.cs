using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject hoverPrompt; // the promt for interacting with this object
    // Start is called before the first frame update
    void Start()
    {
        hoverPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter() // when we enter the collider of this object
    {
        print("Hovering over " + gameObject.name);
        hoverPrompt.SetActive(true); // show the prompt
    }

    private void OnMouseExit() // when we exit the collider of this object
    {
        print("Stopped Hovering " + gameObject.name);
        hoverPrompt.SetActive(false); // hide when not hovering
    }

    private void OnMouseDown() // when we click on the object
    {
        print("You clicked me tehehe");
    }
}

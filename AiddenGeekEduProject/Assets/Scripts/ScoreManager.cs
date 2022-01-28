using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public Text ScoreText; // a reference to the score text on our screen
    [SerializeField]
    public Text SizeText; // a reference to the size text on the screen
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + PlayerMovement.score;
        SizeText.text = "Size: " + FindObjectOfType<PlayerMovement>().size + " " + "Counter: " + PlayerMovement.sizeCounter;
    }
}

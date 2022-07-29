using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossTrigger : MonoBehaviour
{
    public GameObject wall; // wall 1
    public GameObject bossCanvas; // to show/hide boss canvas
    public bool fightingBoss; // to know we're passed through the trigger box and the walls will come up
    //public float shakeDuration; // how long to shake
    public float shakeMagnitude; // how aggressive the shake will be
    public Vector3 initpos;
    // Start is called before the first frame update
    void Start()
    {
        bossCanvas.SetActive(false); // hide this stuff
        initpos = wall.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(fightingBoss == true && wall.transform.position.y < 0)
        {
            wall.transform.localPosition = wall.transform.position + Random.insideUnitSphere * shakeMagnitude;
            wall.transform.position = new Vector3(0, wall.transform.position.y + 1 * Time.deltaTime);
        }
            
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            fightingBoss = true;
            bossCanvas.SetActive(true);
        }
    }
}

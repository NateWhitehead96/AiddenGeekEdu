using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Playables;

public class BossTrigger : MonoBehaviour
{
    public GameObject wall; // wall 
    public GameObject bossCanvas; // to show/hide boss canvas
    public bool fightingBoss; // to know we're passed through the trigger box and the walls will come up
    //public float shakeDuration; // how long to shake
    public float shakeMagnitude; // how aggressive the shake will be
    public PlayableDirector director; // the intro animation for our boss
    public GameObject[] bossPieces; // all of the boss parts
    // our new shake variables, we dont need to set them in the inspector so we'll make them all private
    int direction = 1;
    float left, right;

    public PlayableDirector bossDeath; // for when the boss dies
    public GameObject keyGem; // our key to opening the final door :)
    public ParticleSystem wallDust1, wallDust2; // dusts
    // Start is called before the first frame update
    void Start()
    {
        bossCanvas.SetActive(false); // hide this stuff
        // set "left" and "right"
        left = wall.transform.position.x - 0.1f;
        right = wall.transform.position.x + 0.1f;
        for (int i = 0; i < bossPieces.Length; i++)
        {
            bossPieces[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fightingBoss == true && wall.transform.position.y < 0)
        {
            if(wall.transform.position.x > right)
            {
                direction = -1;
            }
            if(wall.transform.position.x < left)
            {
                direction = 1;
            }
            //wall.transform.localPosition = wall.transform.position + Random.insideUnitSphere * shakeMagnitude;
            wall.transform.position = new Vector3(wall.transform.position.x + direction * 3 * Time.deltaTime, wall.transform.position.y + 2 * Time.deltaTime);
        }
            
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            for (int i = 0; i < bossPieces.Length; i++)
            {
                bossPieces[i].SetActive(true);
            }
            fightingBoss = true;
            bossCanvas.SetActive(true);
            director.Play();
            wallDust1.Play();
            wallDust2.Play();
        }
        GetComponent<BoxCollider2D>().enabled = false; // disable the collider
    }

    public void BossDead()
    {
        bossDeath.Play(); // animation
        StartCoroutine("DeleteBoss");
    }

    IEnumerator DeleteBoss()
    {
        bossCanvas.SetActive(false); // hide canvas
        yield return new WaitForSeconds(6.4f);
        for (int i = 0; i < bossPieces.Length; i++) // hide all the boss pieces
        {
            bossPieces[i].SetActive(false);
        }
        wall.SetActive(false); // hide walls
        keyGem.SetActive(true); // show the gem, could even give it some oomph
        //keyGem.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse); // the oomph
    }
}

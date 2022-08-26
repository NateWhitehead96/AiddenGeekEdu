using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBController : MonoBehaviour
{
    public GameObject[] barriers; // walls blocking levels 2-5
    public GameObject[] doors; // the doors we have in our hub world
    public Sprite openDoorSprite; // the sprite we turn our doors to
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < barriers.Length; i++) // to control what walls are up
        {
            if (GameManager.instance.LevelsBeaten > i + 1)
            {
                barriers[i].SetActive(false);
            }
        }
        for (int i = 0; i < doors.Length; i++) // to control what doors are open
        {
            if(GameManager.instance.LevelsBeaten > i)
            {
                doors[i].GetComponentInChildren<SpriteRenderer>().sprite = openDoorSprite;
            }
        }
    }
}

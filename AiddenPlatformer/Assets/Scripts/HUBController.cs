using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBController : MonoBehaviour
{
    public GameObject[] barriers;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            if (GameManager.instance.LevelsBeaten > i + 1)
            {
                barriers[i].SetActive(false);
            }
        }
    }
}

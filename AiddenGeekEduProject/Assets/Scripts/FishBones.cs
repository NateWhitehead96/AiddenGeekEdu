using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBones : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -6) // if the fishbones go off screen they delete themselves
        {
            Destroy(gameObject);
        }
    }
}

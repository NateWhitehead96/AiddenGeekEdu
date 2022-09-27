using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    public Light2D glow;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            glow.intensity = Random.Range(0.5f, 3.5f);

            timer = Random.Range(0.1f, 0.5f);
        }
        timer -= Time.deltaTime;

        //glow.intensity = Random.Range(0.5f, 3.5f);
    }
}

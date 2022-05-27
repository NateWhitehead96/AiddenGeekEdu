using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubbles : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // full breath restore when we touch a bubble
            other.gameObject.GetComponent<BreathMeter>().currentBreath = other.gameObject.GetComponent<BreathMeter>().maxBreath;
            print("Gained breath");
        }
    }
}

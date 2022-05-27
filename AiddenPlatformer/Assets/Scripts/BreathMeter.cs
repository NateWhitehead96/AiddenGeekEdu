using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathMeter : MonoBehaviour
{
    // access to our player script
    private Player player;
    // access to the new canvas that has our breath meter (or at least just the breath meter)
    public Slider Breath;
    // maybe the post processing volume for showing/hiding effects
    public GameObject ppVolume; // optional right now

    public float maxBreath;
    public float currentBreath;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        Breath.maxValue = maxBreath;
        currentBreath = maxBreath;
        Breath.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.swimming == true)
        {
            Breath.gameObject.SetActive(true); // show the breath meter
            currentBreath -= Time.deltaTime; // slowly melt our breath down
            Breath.value = currentBreath; // this will update the slider bar
            if(currentBreath <= 0)
            {
                
                player.HurtPlayer();
            }
        }
        if(player.swimming == false)
        {
            if(currentBreath <= maxBreath)
            {
                currentBreath += 3 * Time.deltaTime;
                Breath.value = currentBreath;
            }
            else
            {
                Breath.gameObject.SetActive(false); // hide the meter
            }
        }
    }

   
}

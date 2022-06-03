using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering; // this allows us to use the Volume scripts

public class BreathMeter : MonoBehaviour
{
    // access to our player script
    private Player player;
    // access to the new canvas that has our breath meter (or at least just the breath meter)
    public Slider Breath;
    // maybe the post processing volume for showing/hiding effects
    public Volume inWaterVolume; // the post processing volume for our scene in the water
    public Volume outOfWaterVolume; // the post processing volume for our scene out of the water

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
            inWaterVolume.weight = Mathf.Lerp(inWaterVolume.weight, 1, Time.deltaTime); // slowly transition to water volume
            outOfWaterVolume.weight = Mathf.Lerp(outOfWaterVolume.weight, 0, Time.deltaTime);
            if(currentBreath <= 0)
            {
                // may fix this so it doesnt spam player damage
                player.HurtPlayer();
            }
        }
        if(player.swimming == false)
        {
            inWaterVolume.weight = Mathf.Lerp(inWaterVolume.weight, 0, Time.deltaTime); // slowly transition to ofw volume
            outOfWaterVolume.weight = Mathf.Lerp(outOfWaterVolume.weight, 1, Time.deltaTime);
            if (currentBreath <= maxBreath)
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

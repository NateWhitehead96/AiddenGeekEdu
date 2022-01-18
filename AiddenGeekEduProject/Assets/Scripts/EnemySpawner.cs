using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy; // prefab enemy

    public float timer; // keep track how how fast enemies spawn

    public Direction direction; // direction enemy will move in
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0) // when the timer is up
        {
            Instantiate(Enemy, transform.position, transform.rotation); // spawn enemy
            timer = 2; // reset timer
        }
        timer -= Time.deltaTime; // decrease our timer
    }
}

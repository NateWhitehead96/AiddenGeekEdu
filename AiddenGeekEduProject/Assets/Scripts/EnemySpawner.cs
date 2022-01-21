using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy; // prefab enemy

    public float timer; // keep track how how fast enemies spawn

    public Direction direction; // direction enemy will move in

    [SerializeField] // forces unity to display this variable in the editor
    public float bounds; // make sure our spawner will still be within camera bounds
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0) // when the timer is up
        {
            float randomSpot = Random.Range(-bounds, bounds); // find a new x or y value for spawner
            if(direction == Direction.Up || direction == Direction.Down)
            {
                transform.position = new Vector3(randomSpot, transform.position.y); // make sure the spawner has a new random x value
            }
            if(direction == Direction.Left || direction == Direction.Right)
            {
                transform.position = new Vector3(transform.position.x, randomSpot); // make sure the spawner has a new random y value
            }

            GameObject newFish = Instantiate(Enemy, transform.position, transform.rotation); // spawn enemy
            newFish.GetComponent<EnemyMovement>().direction = direction;
            // change the size
            // pick a random color
            timer = 2; // reset timer
        }
        timer -= Time.deltaTime; // decrease our timer
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoscrollingPlatform : MonoBehaviour
{
    public bool playerOn; // to know when the player is on this platform
    public int speed;
    public Transform[] movePoints; // an array of points this platform will move towards
    public int currentPoint; // to know which point the platform is currently going to

    public GameObject Enemies; // array of our enemies we can spawn
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerOn == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[currentPoint].position, speed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, movePoints[currentPoint].position); // calc distance
            if(distance <= 0.1f)
            {
                currentPoint++;
                GameObject newEnemy = Instantiate(Enemies, new Vector3(transform.position.x, transform.position.y + 2), transform.rotation);
                newEnemy.transform.parent = transform; // parent enemy to the platform
                newEnemy.GetComponent<GroundEnemy>().rightBounds = 0.48f; // set bounds of the enemy
                newEnemy.GetComponent<GroundEnemy>().leftBounds = -0.48f;
            }
        }

        Debug.DrawLine(movePoints[0].position, movePoints[1].position);
        Debug.DrawLine(movePoints[1].position, movePoints[2].position);
        Debug.DrawLine(movePoints[2].position, movePoints[3].position);
        Debug.DrawLine(movePoints[3].position, movePoints[4].position);
        Debug.DrawLine(movePoints[4].position, movePoints[5].position);
    }
}

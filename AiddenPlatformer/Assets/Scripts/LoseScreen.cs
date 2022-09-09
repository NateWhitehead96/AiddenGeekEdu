using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    
    public void ReturnToHUB() // this button function will return our player to the HUB World
    {
        GameManager.instance.Lives = 3; // gain 3 lives back
        GameManager.instance.Coins = 0; // lose all coins
        //GameManager.instance.LevelsBeaten = 1; // if we want to reset the player progress >:D
        GameManager.instance.SaveGame(); // Save those new changes
        StartCoroutine(FadeToHUB()); // start a coroutine to do a fade to our hub world
    }

    IEnumerator FadeToHUB()
    {
        FindObjectOfType<TransitionCanvas>().anim.SetBool("fade", true); // make the canavs fade in
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Hubworld");// loading the hubworld
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image HeartOne;
    public Image HeartTwo;
    public Image HeartThree;
    public Text CoinAmount;

    public Text Lives;
    public Text Score;
    public Text Timer;

    public Sprite EmptyHeart;
    public Sprite FullHeart;

    public Player player; // link to the player
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>(); // makes sure player is the player in our scene (optional)
    }

    // Update is called once per frame
    void Update()
    {
        //CoinAmount.text = player.Coins.ToString(); // constantly update our coins
        CoinAmount.text = GameManager.instance.Coins.ToString(); // now feed the coins from GameManager
        Lives.text = GameManager.instance.Lives.ToString();
        Score.text = "Score: " + player.Score.ToString();
        Timer.text = "Time: " + player.levelTimer.ToString("#.00");

        // life checking
        if(player.Health == 3) // we are at full health
        {
            HeartOne.sprite = FullHeart;
            HeartTwo.sprite = FullHeart;
            HeartThree.sprite = FullHeart;
        }
        if (player.Health == 2) // we are at 2 health
        {
            HeartOne.sprite = EmptyHeart;
            HeartTwo.sprite = FullHeart;
            HeartThree.sprite = FullHeart;
        }
        if (player.Health == 1) // we are at 1 health
        {
            HeartOne.sprite = EmptyHeart;
            HeartTwo.sprite = EmptyHeart;
            HeartThree.sprite = FullHeart;
        }
        if (player.Health == 0) // we are at 0 health
        {
            HeartOne.sprite = EmptyHeart;
            HeartTwo.sprite = EmptyHeart;
            HeartThree.sprite = EmptyHeart;
        }
    }
}

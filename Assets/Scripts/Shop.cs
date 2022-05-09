using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public ToScene toScene;
    [SerializeField] private int myCoins = 5;
    public Text CurrentCoins;

    [SerializeField] private int magnet = 5;
    public Text magnetPrice;
    public Text CurrentBaffMagnet;

    [SerializeField] private int jump = 5;
    public Text jumpPrice;
    public Text CurrentBaffJump;

    [SerializeField] private int timer = 5;
    public Text timerPrice;
    public Text CurrentBaffTimer;

    private void Start()
    {
        GetCurrentCoins();
        BaffGetter();
    }

    public void GetCurrentCoins()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            myCoins = PlayerPrefs.GetInt("Coins");
            CurrentCoins.text = myCoins.ToString();
        }
        else
        {
            myCoins = 0;
            CurrentCoins.text = myCoins.ToString();
        }
    }
    public void BaffGetter()
    {
        if (PlayerPrefs.HasKey("MagnetBaff"))
        {
            magnet = PlayerPrefs.GetInt("MagnetBaff");
            if(magnet < 15)
                magnetPrice.text = (PlayerPrefs.GetInt("MagnetBaff") * 15).ToString();
            else
                magnetPrice.text = "MAX";
            CurrentBaffMagnet.text = magnet.ToString();
        }
        else
        {
            magnet = 5;
            magnetPrice.text = (magnet * 15).ToString();
            CurrentBaffMagnet.text = magnet.ToString();
        }
        if (PlayerPrefs.HasKey("JumpBaff"))
        {
            jump = PlayerPrefs.GetInt("MagnetBaff");
            if(jump < 15)
                jumpPrice.text = (PlayerPrefs.GetInt("JumpBaff") * 15).ToString();
            else
                jumpPrice.text = "MAX";
            CurrentBaffJump.text = jump.ToString();
        }
        else
        {
            jump = 5;
            jumpPrice.text = (jump * 15).ToString();
            CurrentBaffJump.text = jump.ToString();
        }
        if (PlayerPrefs.HasKey("TimerBaff"))
        {
            timer = PlayerPrefs.GetInt("TimerBaff");
            if(timer < 15)
                timerPrice.text = (PlayerPrefs.GetInt("TimerBaff") * 15).ToString();
            else
                timerPrice.text = "MAX";
            CurrentBaffTimer.text = timer.ToString();
        }
        else
        {
            timer = 5;
            timerPrice.text = (timer * 15).ToString();
            CurrentBaffTimer.text = timer.ToString();
        }
    }

    public void BuyMagnetBaff()
    {
        if(myCoins >= magnet * 75 && magnet < 15)
        {
            myCoins -= magnet * 75;
            magnet += 2;
            if (magnet == 15)
                magnetPrice.text = "MAX";
            else
                magnetPrice.text = (magnet * 75).ToString();
            CurrentBaffMagnet.text = magnet.ToString();
            PlayerPrefs.SetInt("Coins", myCoins);
            PlayerPrefs.SetInt("MagnetBaff", magnet);
            GetCurrentCoins();
        }
    }
    public void BuyJumpBaff()
    {
        if (myCoins >= jump * 75 && jump < 15)
        {
            myCoins -= jump * 75;
            jump += 2;
            if (jump == 15)
                jumpPrice.text = "MAX";
            else
                jumpPrice.text = (jump * 75).ToString();
            CurrentBaffJump.text = jump.ToString();
            PlayerPrefs.SetInt("Coins", myCoins);
            PlayerPrefs.SetInt("JumpBaff", jump);
            GetCurrentCoins();
        }
    }
    public void BuyTimerBaff()
    {
        if (myCoins >= timer * 75 && timer < 15)
        {
            myCoins -= timer * 75;
            timer += 2;
            if (timer == 15)
                timerPrice.text = "MAX";
            else
               timerPrice.text = (timer * 75).ToString();
            CurrentBaffTimer.text = timer.ToString();
            PlayerPrefs.SetInt("Coins", myCoins);
            PlayerPrefs.SetInt("TimerBaff", timer);
            GetCurrentCoins();
        }
    }
    public void Exit()
    {
        gameObject.SetActive(false);
        toScene.GetCoins();
    }
}

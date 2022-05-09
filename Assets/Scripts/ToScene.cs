using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    public Shop shopScript;
    public AudioSource AS;
    public GameObject shop;
    public GameObject settings;
    public Text logo;
    public Text coins;
    private void Start()
    {
        GetCoins();
    }
    private void Update()
    {
        if (Settings.sounds) AS.volume = 1;
        else AS.volume = 0;
    }
    public void GetCoins()
    {
        if (PlayerPrefs.HasKey("Coins"))
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
        else
            coins.text = "0";
    }
    public void GameScene() //это переход на сцену игры
    {
        logo.text = "Loading...";
        SceneManager.LoadScene("Game");
    }
    public void ShopPanel()
    {
        shop.SetActive(true); //тут активация панельки магаза
    }
    public void SettingsPanel()
    {
        settings.SetActive(true);
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Coins", 5000);
        coins.text = PlayerPrefs.GetInt("Coins").ToString();
        shopScript.BaffGetter();
        shopScript.GetCurrentCoins();
    }
}

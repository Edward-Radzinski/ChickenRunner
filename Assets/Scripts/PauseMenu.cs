using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //�� ��� ������������
    public AudioSource AS;
    public GameObject[] panel;
    public PlayerControl pc;

    public static bool died = false;
    public static bool pause = false;
    private void Start()
    {
        if (Settings.sounds) AS.volume = 1;
        else AS.volume = 0;
        died = false;
        pause = false;
    }
    private void Update()
    {
        if (pause) //��� ����� ������� � ������ �� ������ ����� �-�� Pause()
            Time.timeScale = 0; //��������� �������
        else if(died) //��� ����� ��� ������ ��� ���������� ������ ����
        {   
            AS.Pause();
            Time.timeScale = 0;
            panel[1].SetActive(true);  
        }
        else
            Time.timeScale = 1;
    }
    public void Pause()
    {
        AS.Pause();
        panel[0].SetActive(true);
        pause = true;
    }
    public void UnPause()
    {
        AS.UnPause();
        panel[0].SetActive(false);    
        pause = false;
    }
    public void Restart()
    {
        if (PlayerPrefs.HasKey("Coins"))
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + pc.coins);
        else
            PlayerPrefs.SetInt("Coins", pc.coins);
        AS.Play();
        SceneManager.LoadScene("Game"); //������� ������
    }
    public void Menu()
    {
        if (PlayerPrefs.HasKey("Coins"))
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + pc.coins);
        else
            PlayerPrefs.SetInt("Coins", pc.coins);
        SceneManager.LoadScene("Menu");
    }
}

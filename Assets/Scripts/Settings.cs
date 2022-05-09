using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Button soundsOn;
    public Button soundsOff;
    public Button exit;
    public static bool sounds = true;

    private void Start()
    {
        if (sounds)
            SoundsOn();
        else
            SoundsOff();
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
    public void SoundsOn()
    {
        sounds = true;
        soundsOn.enabled = false;
        soundsOn.GetComponent<Image>().enabled = false;
        soundsOff.enabled = true;
        soundsOff.GetComponent<Image>().enabled = true;
    }
    public void SoundsOff()
    {
        sounds = !true;
        soundsOn.enabled = !false;
        soundsOn.GetComponent<Image>().enabled = !false;
        soundsOff.enabled = !true;
        soundsOff.GetComponent<Image>().enabled = !true;
    }
}

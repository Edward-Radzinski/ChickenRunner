using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] AS;
    public AudioClip clip;

    public bool nextCoin = false;
    public float timer = 0;
    
    private void Update()
    {
        if(SwipeManager.swipeUp || SwipeManager.swipeDown || SwipeManager.swipeLeft || SwipeManager.swipeRight)
        {
            AS[0].PlayOneShot(clip); //прогирать звук один раз
        }
        if(nextCoin)
            Timer();
    }
    private void Timer()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            nextCoin = false;
            AS[1].pitch = 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin" || other.gameObject.tag == "BigCoin") //если по прошествию одной секунды мы взяли еще одну монету то увеличивоем частоту звука
        {
            nextCoin = true;
            timer = 0;
            AS[1].pitch += 0.2f; //частота звука
            AS[1].PlayOneShot(clip);
        }
    }
}

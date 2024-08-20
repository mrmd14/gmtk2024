using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLogo : MonoBehaviour
{
    public float timer = 0;
    public float timerSecond = 0;
    public float interval = .5f;
    public string orginalText;


    public Transform playBTN;

    char[] chars = {'@','!','%','&','$','@' };

    public TextMeshPro text;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerSecond -= Time.deltaTime;

        playBTN.localScale = ( Mathf.Abs(Mathf.Sin(Time.time))*.1f + .9f)*Vector3.one;

        if (timerSecond < 0)
        {
            text.text = orginalText;
            
        }
       
        if (timer <= 0)
        {
            timerSecond = .2f;
            timer = Random.Range( interval, 2*interval);
            var s = orginalText;
            int ind = Random.Range(0, s.Length);
            if (ind == 0) ind = 1;
            s = s.Substring(0, ind - 1) + chars[Random.Range(0, chars.Length)] + s.Substring(ind + 1);
            text.text = s;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    public List<StatSlider> statSliders;


    public Text statInfo;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            statInfo.gameObject.SetActive(statInfo.gameObject.activeInHierarchy);


        }

        for(int i = 1; i < 6; ++i)
        {
            Attributes attribute = (Attributes)i;
            statSliders[i-1].text.text = attribute.ToString();

             var range = GamePlayManager.AttributeInitValMap[attribute];


            float currentValue = AttributeData.values[attribute];



            float scaled  = (currentValue - range.MinVal) / (range.MaxVal - range.MinVal);


            statSliders[i-1].slider.value = Mathf.Lerp(statSliders[i-1].slider.value,scaled,Time.deltaTime);
            statSliders[i - 1].bg.color = debuffManager.hasDebuff(attribute) ? Color.red : Color.white;




        }
    }
}

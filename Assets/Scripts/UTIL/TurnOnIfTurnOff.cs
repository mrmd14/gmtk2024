using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnIfTurnOff : MonoBehaviour
{


    public GameObject target;
    public List< GameObject> turnOnn;



    public void Init()
    {
        foreach (var item in turnOnn)
        {
            print(item);
            item.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!target.activeSelf)
        {
            print("here ");
            foreach(var item in turnOnn)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnIfTurnOff : MonoBehaviour
{


    public GameObject target;
    public List< GameObject> turnOnn;



    public bool TurnOnEastOff = false;

    public static bool isEastActive = true;
    public static bool isWestActive = true;



    public void Init()
    {
        isEastActive = true;
        isWestActive = true;
        foreach (var item in turnOnn)
        {
            print(item);
            item.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((TurnOnEastOff&& !isEastActive)|| (!TurnOnEastOff&& isWestActive))
        {
            
            foreach(var item in turnOnn)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}

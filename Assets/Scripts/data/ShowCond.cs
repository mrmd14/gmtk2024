using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowCond : MonoBehaviour
{
    public GameEvents events;


    public TextMeshPro text;
    // Update is called once per frame
    void Update()
    {

        string s = "";
        foreach(var item in events.mainPool)
        {
            s += item.MakeStr() + "\n\n";
        }
        text.text = s;
        transform.position = Vector3.Lerp(transform.position, transform.position -   Input.mouseScrollDelta.y*Vector3.down,Time.deltaTime*12);
    }
} 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBtnToBox : MonoBehaviour
{
    public Action action = ()=> { };


    public BoxCollider2D box;


    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void DO()
    {
        
        action?.Invoke();
    }



    private void Update()
    {
       
        if (  Input.GetMouseButtonDown(0)&&   box.OverlapPoint(Stars.MousePos))
        {
            DO();
        }
    }
}

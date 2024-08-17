using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBtnToBox : MonoBehaviour
{
    public Action action = ()=> { };



    private void OnMouseDown()
    {
        
        action?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrenceToAgentLink : MonoBehaviour
{

    public bool hovering;

    public SpriteRenderer vfx;
    private void OnEnable()
    {

        hovering = false;
    }


    private void OnMouseEnter()
    {
        hovering = true;

        StageManager.instance.ScrollVal = 0;



    }

    private void OnMouseExit()
    {
        hovering = false;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RefrenceToAgentLink : MonoBehaviour
{

    public bool hovering;
    Collider2D box;
    public SpriteRenderer vfx;
    private void OnEnable()
    {
        box = GetComponent<Collider2D>();
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

    private void Update()
    {
        if (box != null)
        {
            hovering = box.OverlapPoint(Stars.MousePos);


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseParallax : MonoBehaviour
{
    public float CO = .1f;

    private Vector2 lastMousePos;


    private Vector3 startPos;

    private bool OnlyInX = false;

    private void SetPos()
    {


        lastMousePos = Stars.MousePos;

      
    }

    private void OnEnable()
    {
        SetPos();

        startPos = transform.position;

    }

    private void OnDisable()
    {
        if(transform != null && transform.position != null )
        {
            transform.position = startPos;
        }
    }


    private void FixedUpdate()
    {
        var delta = Stars.MousePos - lastMousePos;
        delta = delta.normalized;
        lastMousePos = Stars.MousePos;
        
       
          transform.position += delta.x * CO  * Time.fixedDeltaTime * Vector3.right ;
        
    }


}

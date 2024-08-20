using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SolarMove : MonoBehaviour
{

    public float speed = 3;

    public Transform centerPoint;


    float currentAngel = 0;

     float rad = 0;

    public static float AllSpeedCo = .0003f;

    public bool isEarth = false;


    public Transform NoPlayPos;

    public void GoToNoPlayPos()
    {
        transform.position = NoPlayPos.position;
    }


    private void Awake()
    {
        currentAngel = isEarth ? 30 : Random.Range(0, 100) ;
        rad = Vector2.Distance(transform.position , centerPoint.position);
        
        GoToNoPlayPos();
    }

    private void FixedUpdate()
    {

        if (!GamePlayManager.inGamePlay) return;


        float currentRad = rad + (GamePlayManager.isSunBig ? 1 : 0);

        var target = centerPoint.position + (Mathf.Sin(currentAngel) * currentRad * Vector3.right) + (Mathf.Cos(currentAngel) * currentRad * Vector3.up);

        if (Vector2.Distance(target, transform.position) > .01f)
        {
            transform.position = Vector2.Lerp(transform.position, target, Time.fixedDeltaTime * 10);
        }

        else transform.position = target;




        currentAngel += Time.fixedDeltaTime * speed* AllSpeedCo;
    }
}

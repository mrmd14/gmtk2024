using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarMove : MonoBehaviour
{

    public float speed = 3;

    public Transform centerPoint;


    float currentAngel = 0;

    float rad = 0;

    public static float AllSpeedCo = .01f;


    private void Awake()
    {
        currentAngel += Random.Range(0, 100f);
        rad = Vector2.Distance(transform.position , centerPoint.position);
    }

    private void FixedUpdate()
    {
        transform.position = centerPoint.position + (Mathf.Sin(currentAngel) * rad * Vector3.right) + (Mathf.Cos(currentAngel) * rad * Vector3.up);

        currentAngel += Time.fixedDeltaTime * speed* AllSpeedCo;
    }
}

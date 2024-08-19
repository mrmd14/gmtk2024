using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMove : MonoBehaviour
{
    public Transform a, b;

    public float speed = 5;



    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, b.position) < .01f) transform.position = a.position;
        transform.position = Vector3.MoveTowards(transform.position,b.position ,Time.fixedDeltaTime * speed  );
    }
}

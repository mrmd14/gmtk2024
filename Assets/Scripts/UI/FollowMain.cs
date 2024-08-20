using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMain : MonoBehaviour
{

    
    
    // Update is called once per frame
    void LateUpdate()
    {
        transform.localScale =  ( Camera.main.orthographicSize/ 5f ) *Vector3.one;
    }
}

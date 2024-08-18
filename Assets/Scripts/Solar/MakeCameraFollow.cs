using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCameraFollow : MonoBehaviour
{

    private void OnDisable()
    {
        if (Camera.main != null && Camera.main.transform != null )
        Camera.main.transform.position = Vector3.forward * -10 ;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position ,  transform.position + Vector3.forward * -10, Time.fixedDeltaTime*10);
    }
}
